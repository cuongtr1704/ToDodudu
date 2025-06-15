using MongoDB.Driver;
using Task3.Models;

namespace Task3.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<TodoItem> _todos;

        public MongoService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _todos = db.GetCollection<TodoItem>("Todos");
        }

        public List<TodoItem> GetByUser(string username) =>
            _todos.Find(t => t.Username == username).ToList();

        public TodoItem GetById(string id) =>
            _todos.Find(t => t.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault();

        public void Create(TodoItem item) => _todos.InsertOne(item);

        public void Update(string id, TodoItem item) =>
            _todos.ReplaceOne(t => t.Id == MongoDB.Bson.ObjectId.Parse(id), item);

        public void Delete(string id) =>
            _todos.DeleteOne(t => t.Id == MongoDB.Bson.ObjectId.Parse(id));
    }
}
