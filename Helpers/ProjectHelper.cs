using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;
using System.IO;

namespace SnapSaves.Helpers
{
    public class ProjectHelper
    {
        private readonly MongoDbContext _dbContext;

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

                    projectFiles.Add(new ProjectFile
                    {
                        Path = $"/SnapCode/{projectId}/{filePath}",
                        Content = File.ReadAllText(file),
                        IsDirectory = false
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
                        Path = $"/SnapCode/{projectId}/{directoryPathInProject}",
                        IsDirectory = true
                    });

                    stack.Push((directory, directoryPathInProject));
                }
            }

            return projectFiles;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateProjectFromTemplateAsync(Template template, string userId)
        {
            // Find the template project in MongoDB
            var templateProject = await _dbContext.TemplateProjects.Find(p => p.Id == template.MongoId).FirstOrDefaultAsync();
            if (templateProject == null)
                return (false, "Template project not found in MongoDB.");

            // Create a new project for the user
            var newProject = new Project
            {
                Name = template.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = templateProject.Files.Select(f => new ProjectFile
                {
                    Path = f.Path,
                    Content = f.Content,
                    IsDirectory = f.IsDirectory,
                    Children = f.Children
                }).ToList(),
                InstructionsId = template.InstructionsId
            };

            await _dbContext.Projects.InsertOneAsync(newProject);
            return (true, "");
        }


    }
}
