using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
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

    public ClusterType GetMongoClusterType()
    {
        return _database.Client.Cluster.Description.Type;
    }

    public Task<IClientSessionHandle> StartSessionAsync()
    {
        return _database.Client.StartSessionAsync();
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
    public IMongoCollection<Project> TemplateProjects => _database.GetCollection<Project>("TemplateProjects"); // New collection
}
