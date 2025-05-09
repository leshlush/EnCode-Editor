namespace SnapSaves.Helpers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using SnapSaves.Data;
    using SnapSaves.Helpers;
    using SnapSaves.Models;

    public class ProjectHelperTest
    {
        private readonly ProjectHelper _projectHelper;

        public ProjectHelperTest(ProjectHelper projectHelper)
        {
            _projectHelper = projectHelper;
        }

        public async Task TestCreateProjectFromDirectoryAsync()
        {
            try
            {
                // Define the path to the AppleCatchers folder
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Assets", "AppleCatchers");

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Directory '{directoryPath}' does not exist. Please create it and add some files.");
                    return;
                }

                // Define a test user ID
                var testUserId = "68142b5ea82d7de39c2fc4c9";

                // Call the ProjectHelper to create a project
                var project = await _projectHelper.CreateProjectFromDirectoryAsync(directoryPath, testUserId);

                // Log the results
                Console.WriteLine($"Project created successfully!");
                Console.WriteLine($"Project ID: {project.Id}");
                Console.WriteLine($"Project Name: {project.Name}");
                Console.WriteLine($"Number of Files: {project.Files.Count}");

                foreach (var file in project.Files)
                {
                    Console.WriteLine($"- Path: {file.Path}, IsDirectory: {file.IsDirectory}, Content: {file.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
