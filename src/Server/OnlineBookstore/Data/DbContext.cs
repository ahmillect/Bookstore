using MongoDB.Bson;
using OnlineBookstore.Models;

namespace OnlineBookstore.Data
{
    public class DbContext
    {
        private readonly IMongoDatabase _database;
        private readonly DbConfig _configuration;

        public DbContext(DbConfig configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(_configuration.Connection_String);

            _database = client.GetDatabase(_configuration.Database_Name);
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>(_configuration.Books_Collection_Name);
        public IMongoCollection<User> Users => _database.GetCollection<User>(_configuration.Users_Collection_Name);
        public IMongoCollection<Author> Authors => _database.GetCollection<Author>(_configuration.Authors_Collection_Name);

        public List<AuthorBookCount> GetBookCountsByAuthor()
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match",
                    new BsonDocument
                    {
                        { "AuthorId", new BsonDocument { { "$ne", BsonNull.Value } } }
                    }
                ),
                new BsonDocument("$group",
                    new BsonDocument
                    {
                        { "_id", "$AuthorId" },
                        { "count", new BsonDocument("$sum", 1) }
                    }
                ),
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "Authors" },
                        { "localField", "_id" },
                        { "foreignField", "Id" },
                        { "as", "authorDetails" }
                    }
                ),
                new BsonDocument("$unwind", "$authorDetails"),
                new BsonDocument("$project",
                    new BsonDocument
                    {
                        { "AuthorName", "$authorDetails.Name" },
                        { "BookCount", "$count" }
                    }
                )
            };

            var results = Books.Aggregate<BsonDocument>(pipeline).ToList();

            return results.Select(r => new AuthorBookCount
            {
                AuthorName = r["AuthorName"].AsString,
                BookCount = r["BookCount"].AsInt32
            }).ToList();
        }
    }
}
