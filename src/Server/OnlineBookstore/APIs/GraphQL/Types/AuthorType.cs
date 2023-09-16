using OnlineBookstore.APIs.GraphQL.DataLoaders;
using OnlineBookstore.Models;

namespace OnlineBookstore.GraphQL.Types
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            descriptor.Description("Represents an author with their details.");

            descriptor
                .Field(a => a.Id)
                .Description("The unique identifier of the author.");

            descriptor
                .Field(a => a.Name)
                .Description("The name of the author.");

            descriptor
                .Field(a => a.Bio)
                .Description("The bio of the author.");

            descriptor.Field("books")
                .Resolve(async context =>
                    await context.DataLoader<BooksDataLoader>().LoadAsync(context.Parent<Author>().Id, context.RequestAborted))
                .Type<NonNullType<ListType<BookType>>>()
                .Description("This is a list of books written by the author.");

        }
    }
}
