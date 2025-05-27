using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;
using System.IO;

namespace SnapSaves.Helpers
{
    public class ProjectHelper
    {
        private readonly MongoDbContext _dbContext;
        private static readonly HashSet<string> AlwaysTextExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".snapcode", ".txt", ".xml", ".md", ".csv", ".html", ".htm", ".js", ".css"
        };


        public ProjectHelper(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> CreateProjectFromDirectoryAsync(string directoryPath, string userId)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");
            }

            // Step 1: Create a blank project in MongoDB
            var newProject = new Project
            {
                Name = Path.GetFileName(directoryPath),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = new List<ProjectFile>()
            };

            await _dbContext.Projects.InsertOneAsync(newProject);

            // Step 2: Get the projectId from MongoDB
            var projectId = newProject.Id;
            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("Failed to retrieve the project ID after insertion.");
            }

            // Step 3: Build the project files recursively
            var projectFiles = BuildProjectFiles(directoryPath, projectId);

            // Step 4: Update the project with the files
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

                // Process files in the current directory
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

                // Process subdirectories
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
            // Fetch the template project from MongoDB
            var templateProject = await _dbContext.TemplateProjects.Find(t => t.Id == template.MongoId).FirstOrDefaultAsync();
            if (templateProject == null)
                return (false, "Template project not found in MongoDB.", null);

            // Create a new project for the user (to get a new project ID)
            var newProject = new Project
            {
                Name = string.IsNullOrWhiteSpace(customName) ? template.Name + " (Copy)" : customName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = new List<ProjectFile>(),
                InstructionsId = template.InstructionsId
            };

            await _dbContext.Projects.InsertOneAsync(newProject);

            var newProjectId = newProject.Id;
            var oldProjectId = templateProject.Id;

            // Copy files, replacing the old projectId in the path with the new one (robustly)
            newProject.Files = templateProject.Files.Select(f => new ProjectFile
            {
                Path = ReplaceProjectIdInPath(f.Path, oldProjectId, newProjectId),
                Content = f.Content,
                IsDirectory = f.IsDirectory,
                IsBinary = f.IsBinary
            }).ToList();

            // Update the project with the new files
            await _dbContext.Projects.ReplaceOneAsync(p => p.Id == newProjectId, newProject);

            return (true, "", newProject);
        }

        private static string ReplaceProjectIdInPath(string path, string oldId, string newId)
        {
            // Handles /oldId, /oldId/, /oldId/something, etc.
            if (path == $"/{oldId}")
                return $"/{newId}";
            if (path.StartsWith($"/{oldId}/"))
                return $"/{newId}/{path.Substring(oldId.Length + 2)}";
            // fallback: replace any occurrence (should rarely be needed)
            return path.Replace(oldId, newId);
        }
        private static bool IsBinaryFile(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            if (AlwaysTextExtensions.Contains(ext))
                return false; // Always treat as text

            using var stream = System.IO.File.OpenRead(filePath);
            var buffer = new byte[8000];
            int read = stream.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < read; i++)
                if (buffer[i] == 0)
                    return true;
            return false;
        }

    }
}
