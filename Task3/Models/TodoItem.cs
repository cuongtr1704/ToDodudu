using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Task3.Models
{
    public class TodoItem
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("IsDone")]
        public bool IsDone { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }
    }
}
