using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineBookstore.Models
{
    public class Author
    {
        public Author()
        {
        }

        public Author(string id, string name, string bio)
        {
            Id = id;
            Name = name;
            Bio = bio;
            Books = new List<Book>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }

        [BsonElement("Books")]
        public ICollection<Book> Books { get; set; }
    }
}
