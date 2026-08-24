using Microsoft.AspNetCore.Http;
using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public MembersController(LibraryDbContext context)
        {
            _context = context;
        }

        // Get: api/members
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        //Get: api/members/5

        [HttpGet("{id}")]

        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        //POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMember),
                new { id = member.id },
                member);
        }

        //PUT: api/member/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            if (id != member.id)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        //Delete: api/members/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool MemberExists(int id)
        {
            throw new NotImplementedException();
        }
       
    }
}