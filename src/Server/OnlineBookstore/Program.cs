using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineBookstore.Data;
using OnlineBookstore.GraphQL.Types;
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

builder.Services.AddGraphQLServer()
                .AddMutationConventions()
                .AddAuthorization()
                .AddQueryType<Queries>()
                .AddMutationType<Mutations>()
                .AddType<BookType>()
                .AddType<AuthorType>()
                .AddType<UserType>()
                .AddMongoDbPagingProviders()
                .AddProjections()
                .AddFiltering()
                .AddSorting();

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
