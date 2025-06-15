using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Task3.Models;

namespace Task3.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly PasswordHasher<User> _hasher;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _users = db.GetCollection<User>("Users");
            _hasher = new PasswordHasher<User>();
        }

        public User GetByUsername(string username) =>
            _users.Find(u => u.Username == username).FirstOrDefault();

        public void Create(User user)
        {
            user.Password = _hasher.HashPassword(user, user.Password);
            _users.InsertOne(user);
        }

        public bool ValidatePassword(User user, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, user.Password, inputPassword);
            return result == PasswordVerificationResult.Success;
        }

        public void Update(User user) =>
            _users.ReplaceOne(u => u.Id == user.Id, user);
    }
}
