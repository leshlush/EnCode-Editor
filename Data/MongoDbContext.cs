using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public Task<IClientSessionHandle> StartSessionAsync()
    {
        return _database.Client.StartSessionAsync();
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
    public IMongoCollection<Project> TemplateProjects => _database.GetCollection<Project>("TemplateProjects"); // New collection
}
