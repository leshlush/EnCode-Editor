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

                // Process Templates and Lessons
                foreach (var templateEntry in manifest.Templates)
                {
                    await ProcessTemplateAsync(learningPathDir, templateEntry, learningPath.Id);
                }

                foreach (var lessonEntry in manifest.Lessons)
                {
                    await ProcessLessonAsync(learningPathDir, lessonEntry, learningPath.Id);
                }
            }
        }

        private async Task ProcessTemplateAsync(string learningPathDir, TemplateEntry templateEntry, int learningPathId)
        {
            var templateDir = Path.Combine(learningPathDir, $"Template{templateEntry.Name}");
            if (!Directory.Exists(templateDir))
            {
                Console.WriteLine($"Template directory '{templateDir}' not found. Skipping...");
                return;
            }

            // Check for "files" and "instructions" subdirectories
            var filesDir = Path.Combine(templateDir, "files");
            var instructionsDir = Path.Combine(templateDir, "instructions");

            if (!Directory.Exists(filesDir))
            {
                Console.WriteLine($"Files directory '{filesDir}' not found. Skipping...");
                return;
            }

            // Create a project from the "files" directory
            var project = await _projectHelper.CreateProjectFromDirectoryAsync(filesDir, userId: null);

            Template template;
            string? instructionsId = null;

            if (Directory.Exists(instructionsDir))
            {
                instructionsId = await SaveInstructionsAsync(instructionsDir, templateEntry.Name);
            }

            // Create the template and link the instructions if present
            template = await _templateHelper.CreateUniversalTemplateAsync(
                project,
                description: $"Template for {templateEntry.Name}"
            );

            if (!string.IsNullOrEmpty(instructionsId))
            {
                template.InstructionsId = instructionsId;
                _context.Templates.Update(template);
                await _context.SaveChangesAsync();
            }

            // Create the LearningItem
            var learningItem = new LearningItemRequest
            {
                Name = templateEntry.Name,
                ItemType = LearningItemType.Template,
                TemplateId = template.Id,
                Position = templateEntry.Position
            };

            await _learningPathHelper.AddLearningItemsAsync(learningPathId, new List<LearningItemRequest> { learningItem });
        }

        private async Task ProcessLessonAsync(string learningPathDir, LessonEntry lessonEntry, int learningPathId)
        {
            var lessonDir = Path.Combine(learningPathDir, $"Lesson{lessonEntry.Name}");
            if (!Directory.Exists(lessonDir))
            {
                Console.WriteLine($"Lesson directory '{lessonDir}' not found. Skipping...");
                return;
            }

            // Save the lesson directory to wwwroot/lessons/{guid}/
            var lessonFolder = Path.Combine("wwwroot", "lessons", Guid.NewGuid().ToString());
            Directory.CreateDirectory(lessonFolder);

            // Copy all files and subdirectories recursively
            CopyDirectoryRecursively(lessonDir, lessonFolder);

            // Save the relative path to content/index.html as the Location in Lesson
            var lesson = new Lesson
            {
                Location = Path.Combine("lessons", Path.GetFileName(lessonFolder), "index.html"),
                Description = $"Lesson for {lessonEntry.Name}"
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            // Create the LearningItem
            var learningItem = new LearningItemRequest
            {
                Name = lessonEntry.Name,
                ItemType = LearningItemType.Lesson,
                LessonId = lesson.Id,
                Position = lessonEntry.Position
            };

            await _learningPathHelper.AddLearningItemsAsync(learningPathId, new List<LearningItemRequest> { learningItem });
        }

        private async Task<string> SaveInstructionsAsync(string instructionsDir, string templateName)
        {
            try
            {
                var instructionsFolder = Path.Combine("wwwroot", "instructions", Guid.NewGuid().ToString());
                Directory.CreateDirectory(instructionsFolder);

                // Copy all files and subdirectories recursively
                CopyDirectoryRecursively(instructionsDir, instructionsFolder);

                // Save the relative path to content/index.html as the Location in Instructions
                var instructions = new Instructions
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = InstructionsType.Static,
                    Location = Path.Combine("instructions", Path.GetFileName(instructionsFolder), "index.html"),
                    Description = $"Instructions for {templateName}",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Instructions.Add(instructions);
                await _context.SaveChangesAsync();

                return instructions.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing instructions: {ex.Message}");
                throw;
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
                    manifest.Description = line.Substring(12).Trim();
                }
                else if (line.StartsWith("Unit:"))
                {
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
                    var parts = line.Substring(9).Split(';');
                    var templateName = parts[0].Trim();
                    var position = int.Parse(parts[1].Trim());

                    manifest.Templates.Add(new TemplateEntry
                    {
                        Name = templateName,
                        Position = position
                    });
                }
                else if (line.StartsWith("Lesson:"))
                {
                    var parts = line.Substring(7).Split(';');
                    var lessonName = parts[0].Trim();
                    var position = int.Parse(parts[1].Trim());

                    manifest.Lessons.Add(new LessonEntry
                    {
                        Name = lessonName,
                        Position = position
                    });
                }
            }

            return manifest;
        }

        // Helper method to copy directories recursively
        private void CopyDirectoryRecursively(string sourceDir, string destinationDir)
        {
            // Create the destination directory if it doesn't exist
            Directory.CreateDirectory(destinationDir);

            // Copy all files in the current directory
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }

            // Recursively copy all subdirectories
            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                var destSubDir = Path.Combine(destinationDir, Path.GetFileName(subDir));
                CopyDirectoryRecursively(subDir, destSubDir);
            }
        }
    }

    // Helper classes for parsing the manifest
    public class Manifest
    {
        public List<UnitRequest> Units { get; set; } = new List<UnitRequest>();
        public List<TemplateEntry> Templates { get; set; } = new List<TemplateEntry>();
        public List<LessonEntry> Lessons { get; set; } = new List<LessonEntry>();
        public string Description { get; set; } = string.Empty;
    }

    public class TemplateEntry
    {
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
    }

    public class LessonEntry
    {
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
    }
}