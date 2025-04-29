using MongoDB.Driver;
using SnapSaves.Models;

namespace SnapSaves.Data
{
    public class MongoDbSeeder
    {
        private readonly MongoDbContext _dbContext;

        public MongoDbSeeder(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            await SeedTemplateProjects();
        }

        private async Task SeedTemplateProjects()
        {
            var templateProjects = new List<Project>
            {
                new Project
                {
                    Name = "Template 1",
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    Files = new List<ProjectFile>
                    {
                        new ProjectFile
                        {
                            Path = "index.html",
                            Content = "<!DOCTYPE html><html><head><title>Template 1</title></head><body><h1>Welcome to Template 1</h1></body></html>",
                            IsDirectory = false
                        },
                        new ProjectFile
                        {
                            Path = "style.css",
                            Content = "body { font-family: Arial, sans-serif; }",
                            IsDirectory = false
                        }
                    }
                },
                new Project
                {
                    Name = "Template 2",
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    Files = new List<ProjectFile>
                    {
                        new ProjectFile
                        {
                            Path = "main.js",
                            Content = "console.log('Welcome to Template 2');",
                            IsDirectory = false
                        },
                        new ProjectFile
                        {
                            Path = "README.md",
                            Content = "# Template 2\nThis is a sample template project.",
                            IsDirectory = false
                        }
                    }
                }
            };

            // Check if the templates already exist
            var existingTemplates = await _dbContext.TemplateProjects.Find(_ => true).ToListAsync();
            if (!existingTemplates.Any())
            {
                await _dbContext.TemplateProjects.InsertManyAsync(templateProjects);
                Console.WriteLine("Template projects seeded successfully.");
            }
            else
            {
                Console.WriteLine("Template projects already exist. Skipping seeding.");
            }
        }
    }
}
