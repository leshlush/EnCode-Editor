// Data/MongoDbSeeder.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SnapSaves.Models;

namespace SnapSaves.Data
{
    public class MongoDbSeeder
    {
        private readonly MongoDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public MongoDbSeeder(MongoDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            // Check if database exists and has collections
            var databaseExists = (await _context.Database.ListCollectionNamesAsync())
                .Any();

            if (!databaseExists)
            {
                
                await SeedProjects();
            }
        }

       

        private async Task SeedProjects()
        {
            var testUserId = (await _context.Users.Find(u => u.Username == "testuser").FirstOrDefaultAsync())?.Id;

            if (testUserId == null) return;

            var projects = new List<Project>
            {
                new Project
                {
                    Name = "Sample Project 1",
                    UserId = testUserId,
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    Files = new List<ProjectFile>
                    {
                        new ProjectFile
                        {
                            Path = "main.js",
                            Content = "console.log('Hello SnapSaves!');",
                            IsDirectory = false
                        },
                        new ProjectFile
                        {
                            Path = "src",
                            IsDirectory = true,
                            Children = new List<ProjectFile>
                            {
                                new ProjectFile
                                {
                                    Path = "src/utils.js",
                                    Content = "export function greet() { return 'Hello'; }",
                                    IsDirectory = false
                                }
                            }
                        }
                    }
                }
            };

            await _context.Projects.InsertManyAsync(projects);
        }

       
    }
}