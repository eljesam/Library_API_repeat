using Microsoft.AspNetCore.Http;
using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // Get: api/books
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        //Get: api/books/5

        [HttpGet("{id}")]

        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var authorExists = await _context.Authors
                .AnyAsync(a => a.id == book.AuthorId);

            if (!authorExists)
            {
                return BadRequest("Author does not exist");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                book);
        }

        //PUT: api/books/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var authorExists = await _context.Authors
                .AnyAsync(a => a.id == book.AuthorId);

            if (!authorExists)
            {
                return BadRequest("Author does not exist");
            }

            _context.Entry(book).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        //Delete: api/books/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool BookExists(int id)
        {
            throw new NotImplementedException();
        }
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.id == id);
        }
    }
}