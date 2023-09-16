using HotChocolate.Authorization;
using OnlineBookstore.Data;
using OnlineBookstore.Data.DTOs.Author;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class AuthorMutation
    {
        [Authorize]
        public async Task<Author> AddAuthor(AuthorInput input, [Service] DbContext dbContext)
        {
            var author = new Author
            {
                Name = input.Name,
                Bio = input.Bio
            };

            await dbContext.Authors.InsertOneAsync(author);
            return author;
        }

        [Authorize]
        public async Task<Author> UpdateAuthor(AuthorUpdateInput input, [Service] DbContext dbContext)
        {
            var filter = Builders<Author>.Filter.Eq(u => u.Id, input.Id);
            var update = Builders<Author>.Update
                .Set(u => u.Name, input.Name)
                .Set(u => u.Bio, input.Bio);

            await dbContext.Authors.UpdateOneAsync(filter, update);
            return await dbContext.Authors.Find(u => u.Id == input.Id).FirstOrDefaultAsync();
        }

        [Authorize]
        public async Task<Author> DeleteAuthor(string id, [Service] DbContext dbContext)
        {
            var author = dbContext.Authors.Find(author => author.Id == id).FirstOrDefault();
            await dbContext.Authors.DeleteOneAsync(author => author.Id == id);
            return author;
        }
    }
}
