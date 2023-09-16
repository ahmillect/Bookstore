using OnlineBookstore.Models;

namespace OnlineBookstore.GraphQL.Types
{
    public class AuthorBookCountType : ObjectType<AuthorBookCount>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthorBookCount> descriptor)
        {
            descriptor.Description("Represents the count of books by an author.");

            descriptor
                .Field(b => b.AuthorName)
                .Description("The name of the author.");

            descriptor
                .Field(b => b.BookCount)
                .Description("The count of books written by the author.");
        }
    }
}
