using System.IO;
using SnapSaves.Data;
using SnapSaves.Models;
using SnapSaves.Helpers;
using Microsoft.EntityFrameworkCore;

namespace SnapSaves.Helpers
{
    public class LearningPathImporterHelper
    {
        private readonly AppIdentityDbContext _context;
        private readonly LearningPathHelper _learningPathHelper;
        private readonly TemplateHelper _templateHelper;
        private readonly ProjectHelper _projectHelper;

        public LearningPathImporterHelper(
            AppIdentityDbContext context,
            LearningPathHelper learningPathHelper,
            TemplateHelper templateHelper,
            ProjectHelper projectHelper)
        {
            _context = context;
            _learningPathHelper = learningPathHelper;
            _templateHelper = templateHelper;
            _projectHelper = projectHelper;
        }

        public async Task ImportLearningPathsAsync(string rootDirectory)
        {
            if (!Directory.Exists(rootDirectory))
            {
                throw new DirectoryNotFoundException($"The directory '{rootDirectory}' does not exist.");
            }

            var learningPathDirectories = Directory.GetDirectories(rootDirectory);

            foreach (var learningPathDir in learningPathDirectories)
            {
                // Extract the meaningful part of the learning path name
                var learningPathName = Path.GetFileName(learningPathDir);
                if (learningPathName.StartsWith("LearningPath"))
                {
                    learningPathName = learningPathName.Substring("LearningPath".Length); // Remove "LearningPath" prefix
                }

                var manifestPath = Path.Combine(learningPathDir, "manifest.txt");

                if (!File.Exists(manifestPath))
                {
                    Console.WriteLine($"Manifest file not found in '{learningPathDir}'. Skipping...");
                    continue;
                }

                // Parse the manifest file
                var manifest = ParseManifest(manifestPath);

                // Create the LearningPath
                var learningPath = await _learningPathHelper.CreateLearningPathAsync(
                    name: learningPathName,
                    description: manifest.Description ?? $"Learning Path: {learningPathName}"
                );

                // Create Units
                await _learningPathHelper.AddUnitsAsync(learningPath.Id, manifest.Units);

                // Create Templates and LearningItems
                foreach (var templateEntry in manifest.Templates)
                {
                    var templateDir = Path.Combine(learningPathDir, $"Template{templateEntry.Name}");
                    if (!Directory.Exists(templateDir))
                    {
                        Console.WriteLine($"Template directory '{templateDir}' not found. Skipping...");
                        continue;
                    }

                    // Check for "files" and "instructions" subdirectories
                    var filesDir = Path.Combine(templateDir, "files");
                    var instructionsDir = Path.Combine(templateDir, "instructions");

                    if (!Directory.Exists(filesDir))
                    {
                        Console.WriteLine($"Files directory '{filesDir}' not found. Skipping...");
                        continue;
                    }

                    // Create a project from the "files" directory
                    var project = await _projectHelper.CreateProjectFromDirectoryAsync(filesDir, userId: null);

                    Template template;
                    if (Directory.Exists(instructionsDir))
                    {
                        // Create a project for the instructions
                        var instructionsProject = await _projectHelper.CreateProjectFromDirectoryAsync(instructionsDir, userId: null);

                        // Create a template with instructions
                        template = await _templateHelper.CreateUniversalTemplateAsync(
                            project,
                            description: $"Template for {templateEntry.Name}",
                            instructionsProject: instructionsProject
                        );
                    }
                    else
                    {
                        // Create a template without instructions
                        template = await _templateHelper.CreateUniversalTemplateAsync(
                            project,
                            description: $"Template for {templateEntry.Name}"
                        );
                    }

                    // Create the LearningItem
                    var learningItem = new LearningItemRequest
                    {
                        Name = templateEntry.Name, // Use the name from the manifest
                        ItemType = LearningItemType.Template,
                        TemplateId = template.Id,
                        Position = templateEntry.Position
                    };

                    await _learningPathHelper.AddLearningItemsAsync(learningPath.Id, new List<LearningItemRequest> { learningItem });
                }
            }
        }

        private Manifest ParseManifest(string manifestPath)
        {
            var manifest = new Manifest();

            var lines = File.ReadAllLines(manifestPath);
            foreach (var line in lines)
            {
                if (line.StartsWith("Description:"))
                {
                    // Parse Description
                    manifest.Description = line.Substring(12).Trim();
                }
                else if (line.StartsWith("Unit:"))
                {
                    // Parse Unit
                    var parts = line.Substring(5).Split(';');
                    var unitName = parts[0].Trim();
                    var rangeParts = parts[1].Split('-');
                    var startPosition = int.Parse(rangeParts[0].Trim());
                    var endPosition = int.Parse(rangeParts[1].Trim());

                    manifest.Units.Add(new UnitRequest
                    {
                        Name = unitName,
                        StartPosition = startPosition,
                        EndPosition = endPosition
                    });
                }
                else if (line.StartsWith("Template:"))
                {
                    // Parse Template
                    var parts = line.Substring(9).Split(';');
                    var templateName = parts[0].Trim();
                    var position = int.Parse(parts[1].Trim());

                    manifest.Templates.Add(new TemplateEntry
                    {
                        Name = templateName,
                        Position = position
                    });
                }
            }

            return manifest;
        }
    }

    // Helper classes for parsing the manifest
    public class Manifest
    {
        public List<UnitRequest> Units { get; set; } = new List<UnitRequest>();
        public List<TemplateEntry> Templates { get; set; } = new List<TemplateEntry>();
        public string Description { get; set; } = string.Empty;
    }

    public class TemplateEntry
    {
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
    }
}