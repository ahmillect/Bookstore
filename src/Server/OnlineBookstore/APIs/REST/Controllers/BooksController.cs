using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Books;

namespace OnlineBookstore.APIs.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookService.GetBooks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<Book>> GetBookByTitle(string title)
        {
            var book = await _bookService.GetBookByTitle(title);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var newBook = await _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(string id, Book book)
        {
            var updatedBook = await _bookService.UpdateBook(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(string id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
