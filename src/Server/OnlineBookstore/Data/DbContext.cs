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
    }
}
