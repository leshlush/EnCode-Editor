using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;
using System.IO;

namespace SnapSaves.Helpers
{
    public class ProjectHelper
    {
        private readonly MongoDbContext _dbContext;
        private readonly AppIdentityDbContext _identityDbContext;
        private static readonly HashSet<string> AlwaysTextExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".snapcode", ".txt", ".xml", ".md", ".csv", ".html", ".htm", ".js", ".css"
        };

        public ProjectHelper(MongoDbContext dbContext, AppIdentityDbContext identityDbContext)
        {
            _dbContext = dbContext;
            _identityDbContext = identityDbContext;
        }

        public async Task<Project> CreateProjectFromDirectoryAsync(string directoryPath, string userId)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");

            var newProject = new Project
            {
                Name = Path.GetFileName(directoryPath),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = new List<ProjectFile>()
            };

            await _dbContext.Projects.InsertOneAsync(newProject);

            var projectId = newProject.Id;
            if (string.IsNullOrEmpty(projectId))
                throw new Exception("Failed to retrieve the project ID after insertion.");

            var projectFiles = BuildProjectFiles(directoryPath, projectId);

            newProject.Files = projectFiles;
            await _dbContext.Projects.ReplaceOneAsync(p => p.Id == projectId, newProject);

            return newProject;
        }

        private List<ProjectFile> BuildProjectFiles(string directoryPath, string projectId)
        {
            var projectFiles = new List<ProjectFile>();
            var stack = new Stack<(string CurrentPath, string ParentPath)>();

            stack.Push((directoryPath, string.Empty));

            while (stack.Count > 0)
            {
                var (currentPath, parentPath) = stack.Pop();

                var files = Directory.GetFiles(currentPath);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var filePath = $"{parentPath}/{fileName}".TrimStart('/');

                    bool isBinary = IsBinaryFile(file);
                    byte[] bytes = System.IO.File.ReadAllBytes(file);
                    string content = isBinary
                        ? Convert.ToBase64String(bytes)
                        : System.Text.Encoding.UTF8.GetString(bytes);

                    projectFiles.Add(new ProjectFile
                    {
                        Path = $"/{projectId}/{filePath}",
                        Content = content,
                        IsDirectory = false,
                        IsBinary = isBinary
                    });
                }

                var directories = Directory.GetDirectories(currentPath);
                foreach (var directory in directories)
                {
                    var directoryName = Path.GetFileName(directory);
                    var directoryPathInProject = $"{parentPath}/{directoryName}".TrimStart('/');

                    projectFiles.Add(new ProjectFile
                    {
                        Path = $"/{projectId}/{directoryPathInProject}",
                        IsDirectory = true
                    });

                    stack.Push((directory, directoryPathInProject));
                }
            }

            return projectFiles;
        }

        public async Task<(bool Success, string ErrorMessage, Project? Project)> CreateProjectFromTemplateAsync(
            Template template, string userId, string? customName = null)
        {
            var templateProject = await _dbContext.TemplateProjects.Find(t => t.Id == template.MongoId).FirstOrDefaultAsync();
            if (templateProject == null)
                return (false, "Template project not found in MongoDB.", null);

            var newProject = CreateProjectInstance(template, templateProject, userId, customName);

            await _dbContext.Projects.InsertOneAsync(newProject);

            newProject.Files = CopyTemplateFiles(templateProject, newProject.Id);
            await _dbContext.Projects.ReplaceOneAsync(p => p.Id == newProject.Id, newProject);

            return (true, "", newProject);
        }

        public async Task<(bool Success, string ErrorMessage, Project? Project)> CreateAnonymousProjectFromUniversalTemplateAsync(string templateId)
        {
            var template = _identityDbContext.Templates.FirstOrDefault(t =>
                t.MongoId == templateId &&
                t.IsUniversal == true &&
                t.AllowAnonymousAccess == true);

            if (template == null)
                return (false, "Template not found or not available for anonymous users.", null);

            var templateProject = await _dbContext.TemplateProjects.Find(t => t.Id == template.MongoId).FirstOrDefaultAsync();
            if (templateProject == null)
                return (false, "Template project not found in MongoDB.", null);

            var newProject = CreateProjectInstance(template, templateProject, null, template.Name + " (Anonymous Copy)");

            await _dbContext.Projects.InsertOneAsync(newProject);

            newProject.Files = CopyTemplateFiles(templateProject, newProject.Id);
            await _dbContext.Projects.ReplaceOneAsync(p => p.Id == newProject.Id, newProject);

            var projectRecord = new ProjectRecord
            {
                MongoId = newProject.Id,
                UserId = null,
                CourseId = null,
                CreatedAt = newProject.CreatedAt
            };
            _identityDbContext.ProjectRecords.Add(projectRecord);
            await _identityDbContext.SaveChangesAsync();

            return (true, "", newProject);
        }

        private Project CreateProjectInstance(Template template, Project templateProject, string? userId, string? customName)
        {
            return new Project
            {
                Name = string.IsNullOrWhiteSpace(customName) ? template.Name + " (Copy)" : customName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = new List<ProjectFile>(),
                InstructionsId = template.InstructionsId
            };
        }

        private List<ProjectFile> CopyTemplateFiles(Project templateProject, string newProjectId)
        {
            var oldProjectId = templateProject.Id;
            return templateProject.Files.Select(f => new ProjectFile
            {
                Path = ReplaceProjectIdInPath(f.Path, oldProjectId, newProjectId),
                Content = f.Content,
                IsDirectory = f.IsDirectory,
                IsBinary = f.IsBinary
            }).ToList();
        }

        private static string ReplaceProjectIdInPath(string path, string oldId, string newId)
        {
            if (path == $"/{oldId}")
                return $"/{newId}";
            if (path.StartsWith($"/{oldId}/"))
                return $"/{newId}/{path.Substring(oldId.Length + 2)}";
            return path.Replace(oldId, newId);
        }

        private static bool IsBinaryFile(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            if (AlwaysTextExtensions.Contains(ext))
                return false;

            using var stream = System.IO.File.OpenRead(filePath);
            var buffer = new byte[8000];
            int read = stream.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < read; i++)
                if (buffer[i] == 0)
                    return true;
            return false;
        }

        public async Task<(bool Success, string ErrorMessage, Project? Project)> CopyProjectAsync(string projectId, string userId, int? courseId = null)
        {
            // 1. Fetch the existing project from MongoDB
            var existingProject = await _dbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (existingProject == null)
                return (false, "Original project not found.", null);

            // 2. Create a deep copy and update fields
            var newProject = new Project
            {
                Name = existingProject.Name + " (Copy)",
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = existingProject.Files
                    .Select(f => new ProjectFile
                    {
                        Path = f.Path, // We'll update with new ID after insert
                        Content = f.Content,
                        IsDirectory = f.IsDirectory,
                        IsBinary = f.IsBinary
                    }).ToList(),
                InstructionsId = existingProject.InstructionsId
            };

            // 3. Insert the new project to get a new Id
            await _dbContext.Projects.InsertOneAsync(newProject);

            // 4. Update file paths to use the new project Id
            var newProjectId = newProject.Id;
            newProject.Files = existingProject.Files
                .Select(f => new ProjectFile
                {
                    Path = ReplaceProjectIdInPath(f.Path, existingProject.Id, newProjectId),
                    Content = f.Content,
                    IsDirectory = f.IsDirectory,
                    IsBinary = f.IsBinary
                }).ToList();

            // 5. Update the project in MongoDB with the new file paths
            await _dbContext.Projects.ReplaceOneAsync(p => p.Id == newProjectId, newProject);

            // 6. Create a new ProjectRecord in MySQL
            var projectRecord = new ProjectRecord
            {
                MongoId = newProjectId,
                UserId = userId,
                CourseId = courseId,
                CreatedAt = newProject.CreatedAt
            };
            _identityDbContext.ProjectRecords.Add(projectRecord);

            // 7. Copy TemplateProject associations (if any)
            var originalTemplateProjects = _identityDbContext.TemplateProjects
                .Where(tp => tp.ProjectMongoId == projectId)
                .ToList();

            foreach (var origTp in originalTemplateProjects)
            {
                var newTp = new TemplateProject
                {
                    TemplateId = origTp.TemplateId,
                    ProjectMongoId = newProjectId
                };
                _identityDbContext.TemplateProjects.Add(newTp);
            }

            await _identityDbContext.SaveChangesAsync();

            return (true, "", newProject);
        }
    }
}