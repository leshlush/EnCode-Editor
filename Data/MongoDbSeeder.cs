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
            var testUser = await _context.Users.Find(u => u.Username == "testuser").FirstOrDefaultAsync();

            if (testUser == null) return;

            var projects = new List<Project>
    {
        new Project
        {
            Name = "Test Project 1",
            UserId = testUser.Id,
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            Files = new List<ProjectFile>
            {
                new ProjectFile
                {
                    Path = "index.html",
                    Content = "<!DOCTYPE html><html><head><title>Test</title></head><body>Hello World</body></html>",
                    IsDirectory = false
                }
            }
        },
        new Project
        {
            Name = "Test Project 2",
            UserId = testUser.Id,
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            Files = new List<ProjectFile>
            {
                new ProjectFile
                {
                    Path = "app.js",
                    Content = "console.log('Test Project 2');",
                    IsDirectory = false
                }
            }
        }
    };

            await _context.Projects.InsertManyAsync(projects);
        }


       
    }
}