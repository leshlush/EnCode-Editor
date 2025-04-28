// Data/MongoDbContext.cs
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SnapSaves.Models;

namespace SnapSaves.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        // Public property for Users collection
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        // Public property for Projects collection
        public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");

        // Access to the database if needed elsewhere
        public IMongoDatabase Database => _database;
    }
}