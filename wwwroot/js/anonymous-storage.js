class AnonymousStorageManager {
    constructor() {
        this.STORAGE_KEY = 'snapcode_anonymous_projects';
        this.EXPIRY_DAYS = 30;
    }

    getAnonymousProjects() {
        try {
            const stored = localStorage.getItem(this.STORAGE_KEY);
            return stored ? JSON.parse(stored) : {};
        } catch (error) {
            console.error('Error reading anonymous projects from localStorage:', error);
            return {};
        }
    }

    getAnonymousProject(templateId) {
        const projects = this.getAnonymousProjects();
        const project = projects[templateId];

        if (!project) return null;

        // Check if expired
        if (new Date() > new Date(project.expiresAt)) {
            this.removeAnonymousProject(templateId);
            return null;
        }

        return project;
    }

    saveAnonymousProject(templateId, projectData) {
        try {
            const projects = this.getAnonymousProjects();
            projects[templateId] = {
                ...projectData,
                lastAccessed: new Date().toISOString()
            };
            localStorage.setItem(this.STORAGE_KEY, JSON.stringify(projects));
            return true;
        } catch (error) {
            console.error('Error saving anonymous project to localStorage:', error);
            return false;
        }
    }

    removeAnonymousProject(templateId) {
        try {
            const projects = this.getAnonymousProjects();
            delete projects[templateId];
            localStorage.setItem(this.STORAGE_KEY, JSON.stringify(projects));
            return true;
        } catch (error) {
            console.error('Error removing anonymous project:', error);
            return false;
        }
    }

    cleanupExpiredProjects() {
        try {
            const projects = this.getAnonymousProjects();
            const now = new Date();
            let hasChanges = false;

            for (const [templateId, project] of Object.entries(projects)) {
                if (now > new Date(project.expiresAt)) {
                    delete projects[templateId];
                    hasChanges = true;
                }
            }

            if (hasChanges) {
                localStorage.setItem(this.STORAGE_KEY, JSON.stringify(projects));
            }
        } catch (error) {
            console.error('Error cleaning up expired projects:', error);
        }
    }

    getDaysUntilExpiry(templateId) {
        const project = this.getAnonymousProject(templateId);
        if (!project) return 0;

        const now = new Date();
        const expiry = new Date(project.expiresAt);
        const diffTime = expiry - now;
        return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    }
}

// Global instance
window.anonymousStorage = new AnonymousStorageManager();

// Clean up expired projects on page load
document.addEventListener('DOMContentLoaded', function () {
    window.anonymousStorage.cleanupExpiredProjects();
});