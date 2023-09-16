using OnlineBookstore.Data;
using OnlineBookstore.Models;
using MongoDB.Driver;
using OnlineBookstore.Services.Authors;

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
                .Field(b => b.AuthorId)
                .Name("author")
                .Resolve(
                    async ctx =>
                    {
                        var authorService = ctx.Service<AuthorService>();
                        return await authorService.GetAuthorById(ctx.Parent<Book>().AuthorId);
                    }
                )
                .Serial()
                .Type<AuthorType>()
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
