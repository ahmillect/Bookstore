using OnlineBookstore.APIs.GraphQL.DataLoaders;
using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.GraphQL.Types
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Description("Represents a book with its details.");

            descriptor
                .Field(b => b.Id)
                .Description("The unique identifier of the book.");

            descriptor
                .Field(b => b.Title)
                .Description("The title of the book.");

            descriptor
                .Field("author")
                .Resolve(async context =>
                {
                    var book = await context.Service<DbContext>().Books.Find(book => book.Id == context.Parent<Book>().Id).FirstOrDefaultAsync();

                    return await context.Service<AuthorDataLoader>().LoadAsync(book.AuthorId, context.RequestAborted);
                })
                .Type<NonNullType<AuthorType>>()
                .Description("The author of the book.");

            descriptor
                .Field(b => b.Description)
                .Description("The description of the book.");

            descriptor
                .Field(b => b.Price)
                .Description("The price of the book.");

            descriptor
                .Field(b => b.PublishedDate)
                .Description("The published date of the book.");
        }
    }
}
