using System.Text;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineBookstore.APIs.GraphQL.DataLoaders;
using OnlineBookstore.APIs.GraphQL.Types.Mutations;
using OnlineBookstore.APIs.GraphQL.Types.Queries;
using OnlineBookstore.Data;
using OnlineBookstore.GraphQL.Types;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Auth;
using OnlineBookstore.Services.Authors;
using OnlineBookstore.Services.Books;
using OnlineBookstore.Services.Users;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DbConfig(builder.Configuration["ConnectionStrings:MongoDb"]);
builder.Configuration.Bind(dbConfig);

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<DbContext>()
                .AddSingleton(dbConfig)
                .AddTransient<IBookService, BookService>()
                .AddTransient<IAuthorService, AuthorService>()
                .AddTransient<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

builder.Services.AddGraphQLServer()
                .AddAuthorization()
                .AddMutationConventions()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<Book>()
                .AddType<Author>()
                .AddType<User>()
                .AddType<BookType>()
                .AddType<AuthorType>()
                .AddType<UserType>()
                .AddType<AuthorBookCountType>()
                .AddTypeExtension<BookQuery>()
                .AddTypeExtension<AuthorQuery>()
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<BookMutation>()
                .AddTypeExtension<AuthorMutation>()
                .AddTypeExtension<UserMutation>()
                .AddDataLoader<AuthorDataLoader>()
                .AddDataLoader<BooksDataLoader>()
                .AddMongoDbPagingProviders()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCorsPolicy");

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.Use((context, next) =>
{
    context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
    return next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL().WithOptions(new GraphQLServerOptions
{
    EnableBatching = true,

});

app.Run();
