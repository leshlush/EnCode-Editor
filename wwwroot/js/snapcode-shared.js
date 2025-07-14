function saveProject() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (jdkFrame && jdkFrame.contentWindow && typeof jdkFrame.contentWindow.saveProject === 'function') {
        jdkFrame.contentWindow.saveProject();
    } else {
        alert('Save function not available in JDK view.');
    }
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

function showShareLinkModal() {
    var jdkFrame = document.getElementById('jdk-iframe');
    if (
        jdkFrame &&
        jdkFrame.contentWindow &&
        typeof jdkFrame.contentWindow.getProjectInfoForShareLink === 'function'
    ) {
        jdkFrame.contentWindow.getProjectInfoForShareLink(function (projectInfo) {
            if (!projectInfo || !projectInfo.userId || !projectInfo.projectId) {
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
                    if (data && data.url) {
                        // Copy to clipboard
                        navigator.clipboard.writeText(data.url);
                        showShareLinkModalContent(data.url, true);
                    } else {
                        showShareLinkModalContent('Failed to get or create share link.', false);
                    }
                })
                .catch(err => {
                    showShareLinkModalContent('Error: ' + err, false);
                });
        });
    } else {
        alert('Share link function not available in JDK view.');
    }
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

