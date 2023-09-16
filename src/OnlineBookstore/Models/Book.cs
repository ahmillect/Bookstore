using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using OnlineBookstore.APIs.GraphQL.Interfaces;

namespace OnlineBookstore.Models
{
    public class Book : ISearchResultType
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

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
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
