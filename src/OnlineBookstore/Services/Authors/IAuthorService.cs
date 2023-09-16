using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Authors
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(string id);
        Task<Author> GetAuthorByName(string name);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(string id, Author author);
        void DeleteAuthor(string id);
    }
}
