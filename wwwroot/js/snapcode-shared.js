function saveProject() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.saveProject === 'function') {
        jdkFrame.contentWindow.saveProject();
    } else {
        alert('Save function not available in JDK view.');
    }
}

function saveAnonymousProgress() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.saveProject === 'function') {
        jdkFrame.contentWindow.saveProject();
    } else {
        alert('Save function not available in JDK view.');
    }
}

function createAnonymousShareLink() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.saveProject === 'function') {
        // Set a flag to indicate this is a silent save for share link creation
        window.savingForShareLink = true;
        
        // Also try to communicate this to the iframe via postMessage
        try {
            jdkFrame.contentWindow.postMessage({type: 'setSilentSave', value: true}, '*');
        } catch (e) {
            console.log('Could not send postMessage to iframe:', e);
        }
        
        // First save the project silently
        jdkFrame.contentWindow.saveProject();
        
        // Then get project info and create share link
        if (typeof jdkFrame.contentWindow.getProjectInfoForShareLink === 'function') {
            jdkFrame.contentWindow.getProjectInfoForShareLink(function (projectInfo) {
                if (!projectInfo || !projectInfo.projectId) {
                    window.savingForShareLink = false; // Reset flag
                    // Reset iframe flag too
                    try {
                        jdkFrame.contentWindow.postMessage({type: 'setSilentSave', value: false}, '*');
                    } catch (e) {}
                    alert('Could not get project info for anonymous share link.');
                    return;
                }

                // Get template ID from context
                var templateId = getTemplateIdFromContext();

                fetch('/api/ProjectsApi/create-anonymous-sharelink', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        projectId: projectInfo.projectId,
                        templateId: templateId
                    })
                })
                .then(response => response.json())
                .then(data => {
                    window.savingForShareLink = false; // Reset flag
                    // Reset iframe flag too
                    try {
                        jdkFrame.contentWindow.postMessage({type: 'setSilentSave', value: false}, '*');
                    } catch (e) {}
                    
                    if (data && data.shareUrl && data.token) {
                        // Save to localStorage for future access
                        window.anonymousStorage.saveAnonymousProject(templateId, {
                            shareToken: data.token,
                            projectId: projectInfo.projectId,
                            expiresAt: data.expiresAt,
                            shareUrl: data.shareUrl
                        });

                        // Show the modal with the share link
                        showAnonymousShareModal(data.shareUrl, data.expiresAt);
                    } else {
                        alert('Failed to create anonymous share link.');
                    }
                })
                .catch(err => {
                    window.savingForShareLink = false; // Reset flag
                    // Reset iframe flag too
                    try {
                        jdkFrame.contentWindow.postMessage({type: 'setSilentSave', value: false}, '*');
                    } catch (e) {}
                    alert('Error creating anonymous share link: ' + err);
                });
            });
        }
    } else {
        alert('Anonymous share link function not available in JDK view.');
    }
}

function showShareLinkModal() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.saveProject === 'function') {
        // Set a flag to indicate this is a silent save for share link creation
        window.savingForShareLink = true;
        
        // First save the project silently
        jdkFrame.contentWindow.saveProject();
        
        // Then get project info and create/get share link
        if (typeof jdkFrame.contentWindow.getProjectInfoForShareLink === 'function') {
            jdkFrame.contentWindow.getProjectInfoForShareLink(function (projectInfo) {
                if (!projectInfo || !projectInfo.userId || !projectInfo.projectId) {
                    window.savingForShareLink = false; // Reset flag
                    alert('Could not get project info for share link.');
                    return;
                }

                // Try to get an existing share link first
                fetch('/api/ProjectsApi/get-sharelink?projectId=' + encodeURIComponent(projectInfo.projectId))
                    .then(response => {
                        if (response.ok) return response.json();
                        // If not found, create one
                        return fetch('/api/ProjectsApi/create-sharelink', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify({
                                userId: projectInfo.userId,
                                projectId: projectInfo.projectId
                            })
                        }).then(r => r.json());
                    })
                    .then(data => {
                        window.savingForShareLink = false; // Reset flag
                        if (data && data.url) {
                            showShareLinkModalContent(data.url, false);
                        } else {
                            showShareLinkModalContent('Failed to get or create share link.', false);
                        }
                    })
                    .catch(err => {
                        window.savingForShareLink = false; // Reset flag
                        showShareLinkModalContent('Error: ' + err, false);
                    });
            });
        }
    } else {
        alert('Share link function not available in JDK view.');
    }
}

function showAnonymousShareModal(shareUrl, expiresAt) {
    // Remove existing modal
    var existing = document.getElementById('anonymousShareModal');
    if (existing) existing.remove();

    var expiryDate = new Date(expiresAt).toLocaleDateString();
    var daysLeft = Math.ceil((new Date(expiresAt) - new Date()) / (1000 * 60 * 60 * 24));

    var modal = document.createElement('div');
    modal.id = 'anonymousShareModal';
    modal.style.cssText = `
        position: fixed; left: 0; top: 0; width: 100vw; height: 100vh;
        background: rgba(0,0,0,0.4); display: flex; align-items: center;
        justify-content: center; z-index: 9999;
    `;

    modal.innerHTML = `
        <div style="background:#fff;padding:2rem;border-radius:8px;max-width:90vw;box-shadow:0 2px 16px #0003;">
            <h5 style="margin-top:0;">Your Anonymous Project Share Link</h5>
            <div class="alert alert-info">
                <i class="fas fa-info-circle me-1"></i>
                This link will allow you to access your project for <strong>${daysLeft} more days</strong>. 
                Create an account to save your work permanently!
            </div>
            <div class="mb-3">
                <label class="form-label">Share Link:</label>
                <div class="input-group">
                    <input type="text" class="form-control" value="${shareUrl}" readonly id="anonymousShareUrl">
                    <button class="btn btn-outline-secondary" onclick="copyAnonymousShareLink('${shareUrl}', this)">
                        <i class="fas fa-copy"></i> Copy
                    </button>
                </div>
            </div>
            <div class="text-muted mb-3">
                <small>
                    <i class="fas fa-calendar-alt me-1"></i>
                    Expires: ${expiryDate}
                </small>
            </div>
            <div class="d-flex justify-content-between">
                <button type="button" class="btn btn-secondary" onclick="document.getElementById('anonymousShareModal').remove()">Close</button>
                <a href="/Auth/Register" class="btn btn-primary">Create Account</a>
            </div>
        </div>
    `;
    document.body.appendChild(modal);
}

function copyAnonymousShareLink(url, button) {
    navigator.clipboard.writeText(url);
    button.innerHTML = '<i class="fas fa-check"></i> Copied!';
    setTimeout(() => {
        button.innerHTML = '<i class="fas fa-copy"></i> Copy';
    }, 2000);
}

// Add these functions to handle anonymous storage
window.anonymousStorage = {
    getAnonymousProjects: function() {
        try {
            return JSON.parse(localStorage.getItem('snapcode_anonymous_projects') || '{}');
        } catch (error) {
            console.error('Error reading anonymous projects:', error);
            return {};
        }
    },
    
    saveAnonymousProject: function(templateId, projectData) {
        try {
            var projects = this.getAnonymousProjects();
            projects[templateId] = {
                ...projectData,
                lastAccessed: new Date().toISOString()
            };
            localStorage.setItem('snapcode_anonymous_projects', JSON.stringify(projects));
        } catch (error) {
            console.error('Error saving anonymous project:', error);
        }
    }
};

// Helper function for getting template ID
function getTemplateIdFromContext() {
    // Try to get from URL params first
    var urlParams = new URLSearchParams(window.location.search);
    var templateId = urlParams.get('templateId');
    
    if (templateId) {
        return templateId;
    }
    
    // Try to get from the current project ID or other context
    var projectId = urlParams.get('projectId');
    if (projectId) {
        return projectId; // Use projectId as templateId for anonymous projects
    }
    
    return 'unknown';
}

function saveACopy(courseId) {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.getProjectInfoForShareLink === 'function') {
        jdkFrame.contentWindow.getProjectInfoForShareLink(function (projectInfo) {
            if (!projectInfo || !projectInfo.userId || !projectInfo.projectId) {
                alert('Could not get project info for saving a copy.');
                return;
            }
            var payload = {
                userId: projectInfo.userId,
                projectId: projectInfo.projectId
            };
            if (courseId) {
                payload.courseId = courseId;
            }
            fetch('/api/ProjectsApi/save-a-copy', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            })
                .then(response => response.json())
                .then(data => {
                    if (data.url) {
                        showCopyModal(data.message, data.url);
                    } else {
                        alert('Failed to save a copy: ' + (data.message || 'Unknown error'));
                    }
                })
                .catch(err => {
                    alert('Error saving a copy: ' + err);
                });
        });
    } else {
        alert('Save-a-copy function not available in JDK view.');
    }
}

// Helper to show a modal with a clickable/copyable link
function showCopyModal(message, url) {
    // Remove any existing modal
    var existing = document.getElementById('saveCopyModal');
    if (existing) existing.remove();

    var modal = document.createElement('div');
    modal.id = 'saveCopyModal';
    modal.style.position = 'fixed';
    modal.style.left = 0;
    modal.style.top = 0;
    modal.style.width = '100vw';
    modal.style.height = '100vh';
    modal.style.background = 'rgba(0,0,0,0.4)';
    modal.style.display = 'flex';
    modal.style.alignItems = 'center';
    modal.style.justifyContent = 'center';
    modal.style.zIndex = 9999;

    modal.innerHTML = `
        <div style="background:#fff;padding:2rem;border-radius:8px;max-width:90vw;box-shadow:0 2px 16px #0003;">
            <h5 style="margin-top:0;">${message}</h5>
            <p>
                <a href="${url}" target="_blank" style="word-break:break-all;">${url}</a>
            </p>
            <button onclick="navigator.clipboard.writeText('${url}');this.innerText='Copied!';" class="btn btn-sm btn-outline-primary">Copy Link</button>
            <button onclick="document.getElementById('saveCopyModal').remove();" class="btn btn-sm btn-secondary ms-2">Close</button>
        </div>
    `;
    document.body.appendChild(modal);
}

function showShareLinkModalContent(url, copied) {
    // Remove any existing modal
    var existing = document.getElementById('shareLinkModal');
    if (existing) existing.remove();

    var modal = document.createElement('div');
    modal.id = 'shareLinkModal';
    modal.style.position = 'fixed';
    modal.style.left = 0;
    modal.style.top = 0;
    modal.style.width = '100vw';
    modal.style.height = '100vh';
    modal.style.background = 'rgba(0,0,0,0.4)';
    modal.style.display = 'flex';
    modal.style.alignItems = 'center';
    modal.style.justifyContent = 'center';
    modal.style.zIndex = 9999;

    modal.innerHTML = `
        <div style="background:#fff;padding:2rem;border-radius:8px;max-width:90vw;box-shadow:0 2px 16px #0003;">
            <h5 style="margin-top:0;">Share Link</h5>
            <p>
                <a href="${url}" target="_blank" style="word-break:break-all;">${url}</a>
            </p>
            <div class="mb-2">
                <span class="text-success">${copied ? 'Link copied to clipboard!' : ''}</span>
            </div>
            <button onclick="navigator.clipboard.writeText('${url}');this.nextElementSibling.innerText='Copied!';" class="btn btn-sm btn-outline-primary">Copy Link</button>
            <span style="margin-left:1rem;"></span>
            <button onclick="document.getElementById('shareLinkModal').remove();" class="btn btn-sm btn-secondary">Close</button>
        </div>
    `;
    document.body.appendChild(modal);
}

async function buildProjectJsonFromIndexedDB(projectId, projectName, userId) {
    const files = await readAllProjectFilesFromIndexedDB(projectId);
    const project = {
        Id: projectId,
        Name: projectName,
        UserId: (userId && userId.trim() !== '') ? userId : null, // Ensure null instead of empty string
        Files: files // Flat array, no nesting
    };
    console.log('Project JSON to be sent:', project);
    return project;
}

// Add missing function for navbar modal
function copyAnonymousShareLinkFromModal() {
    var urlInput = document.getElementById('anonymousShareUrl');
    if (urlInput && urlInput.value) {
        navigator.clipboard.writeText(urlInput.value);
        var button = document.querySelector('#anonymousShareModal .btn-outline-secondary');
        if (button) {
            button.innerHTML = '<i class="fas fa-check"></i> Copied!';
            setTimeout(() => {
                button.innerHTML = '<i class="fas fa-copy"></i> Copy';
            }, 2000);
        }
    }
}

