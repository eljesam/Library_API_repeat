using Microsoft.AspNetCore.Http;
using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
        public class LoansController : ControllerBase
        {
            private readonly LibraryDbContext _context;

            public LoansController(LibraryDbContext context)
            {
                _context = context;
            }

            // Get: api/loans
            [HttpGet]

            public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
            {
                return await _context.Loans.ToListAsync();
            }

            //Get: api/loans/5

            [HttpGet("{id}")]

            public async Task<ActionResult<Loan>> GetLoan(int id)
            {
                var loan = await _context.Loans.FindAsync(id);

                if (loan == null)
                {
                    return NotFound();
                }

                return loan;
            }

            //POST: api/loans
            [HttpPost]
            public async Task<ActionResult<Book>> CreateBook(Loan loan)
            {
                var bookExists = await _context.Books
                    .AnyAsync(b => b.Id == loan.BookId);

                if (!bookExists)
                {
                    return BadRequest("Book does not exist");
                }

                var memberExists = await _context.Members
                    .AnyAsync(m => m.id == loan.MemberId);

                if (!memberExists)
                {
                    return BadRequest("Member does not exist");
                }

                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetLoan),
                    new { id = loan.id },
                    loan);
            }

            //PUT: api/loans/5
            [HttpPut("{id}")]

            public async Task<IActionResult> UpdateLoan(int id, Loan loan)
            {
                if (id != loan.id)
                {
                    return BadRequest();
                }

                 var bookExists = await _context.Books
                    .AnyAsync(b => b.Id == loan.BookId);

                if (!bookExists)
                {
                    return BadRequest("Book does not exist");
                }

                var memberExists = await _context.Members
                    .AnyAsync(m => m.id == loan.MemberId);

                if (!memberExists)
                {
                    return BadRequest("Member does not exist");
                }

                _context.Entry(loan).State = EntityState.Modified;


                try
                {
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(id))
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
                var loan = await _context.Loans.FindAsync(id);

                if (loan == null)
                {
                    return NotFound();
                }

                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            private bool LoanExists(int id)
            {
                throw new NotImplementedException();
            }
           
        }
    }
    

