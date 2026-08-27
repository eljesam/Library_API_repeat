using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Books;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services;
using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Get: api/books
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        //Get: api/books/5

        [HttpGet("{id}")]

        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        //POST: api/books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDTO>> CreateBook(CreateBookDTO dto)
        {
            var authorExists = await _bookService.AuthorExistsAsync(dto.AuthorId);


            if (!authorExists)
            {
                return BadRequest("Author does not exist");
            }

            var book = await _bookService.CreateAsync(dto);

            if (book == null)
            {
                return BadRequest("Unable to add book");
            }

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                book);
        }

        //PUT: api/books/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDTO dto)
        {
            var authorExists = await _bookService.AuthorExistsAsync(dto.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Author does npt exist");
            }

            var updated = await _bookService.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        //Delete: api/books/5

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}