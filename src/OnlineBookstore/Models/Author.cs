using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using OnlineBookstore.APIs.GraphQL.Interfaces;

namespace OnlineBookstore.Models
{
    public class Author : ISearchResultType
    {
        public Author()
        {
        }

        public Author(string id, string name, string bio)
        {
            Id = id;
            Name = name;
            Bio = bio;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }
    }
}
