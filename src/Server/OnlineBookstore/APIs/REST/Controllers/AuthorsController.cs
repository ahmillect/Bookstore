using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Authors;

namespace OnlineBookstore.APIs.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _authorService.GetAuthors();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(string id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Author>> GetAuthorByName(string name)
        {
            var author = await _authorService.GetAuthorByName(name);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            var newAuthor = await _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(string id, Author author)
        {
            var updatedAuthor = await _authorService.UpdateAuthor(id, author);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(string id)
        {
            _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}
