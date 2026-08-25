using Microsoft.AspNetCore.Http;
using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Authors;
using Library_API_repeat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_API_repeat.Api.Services.Interfaces;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Get: api/authors
        [HttpGet]

        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authros = await _authorService.GetAllAsync();
            return Ok(authros);
        }

        //Get: api/authors/5

        [HttpGet("{id}")]

        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        //POST: api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(CreateAuthorDTO dto)
        {
            var author = await _authorService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetAuthor),
                new { id = author.Id },
                author);
        }

        //PUT: api/authors/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var updated = await _authorService.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        //Delete: api/authors/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}


   
