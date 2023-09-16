using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineBookstore.Models
{
    public class Book
    {
        public Book()
        {
        }

        public Book(string id, string title, string authorID, string description, decimal price, DateTime publishedDate)
        {
            Id = id;
            Title = title;
            AuthorId = authorID;
            Description = description;
            Price = price;
            PublishedDate = publishedDate;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("AuthorId")]
        public string AuthorId { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("PublishedDate")]
        public DateTime PublishedDate { get; set; }
    }
}
