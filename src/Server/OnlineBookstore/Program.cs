using OnlineBookstore.Data;
using OnlineBookstore.GraphQL.Types;
using OnlineBookstore.Services.Books;
using OnlineBookstore.Services.Users;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DbConfig(builder.Configuration["ConnectionStrings:MongoDb"]);
builder.Configuration.Bind(dbConfig);

builder.Services.AddSingleton<DbContext>()
                .AddSingleton(dbConfig)
                .AddTransient<IBookService, BookService>()
                .AddTransient<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

builder.Services.AddGraphQLServer()
                .AddQueryType<Queries>()
                .AddMutationType<Mutations>()
                .AddType<BookType>()
                .AddType<UserType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
