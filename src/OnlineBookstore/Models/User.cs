using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace OnlineBookstore.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string id, string username, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Books = new List<Book>();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("Books")]
        public ICollection<Book> Books { get; set; }
    }
}
