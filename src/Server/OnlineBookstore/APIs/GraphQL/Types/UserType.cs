using OnlineBookstore.Models;

namespace OnlineBookstore.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents a user with their details.");

            descriptor
                .Field(u => u.Id)
                .Description("The unique identifier of the user.");

            descriptor
                .Field(u => u.Username)
                .Description("The username of the user.");

            descriptor
                .Field(u => u.Email)
                .Description("The email address of the user.");
        }
    }
}
