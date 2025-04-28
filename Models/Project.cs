// Models/Project.cs
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SnapSaves.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("lastModified")]
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        [BsonElement("files")]
        public List<ProjectFile> Files { get; set; } = new List<ProjectFile>();
    }

    public class ProjectFile
    {
        [BsonElement("path")]
        public string Path { get; set; }  // e.g., "src/main.js"

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("isDirectory")]
        public bool IsDirectory { get; set; }

        [BsonElement("children")]
        public List<ProjectFile> Children { get; set; } = new List<ProjectFile>();
    }
}