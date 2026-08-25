using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Members;
using Library_API_repeat.Api.Services.Interfaces;
using Library_API_repeat.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService memberService)
        {
            _membersService = memberService;
        }

        // GET: api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembersDTO>>> GetMembers()
        {
            var members = await _membersService.GetAllAsync();

            return Ok(members);
        }

        // GET: api/members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MembersDTO>> GetMember(int id)
        {
            var member = await _membersService.GetByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult<MembersDTO>> CreateMember(CreateMembersDTO dto)
        {
            var member = await _membersService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetMember),
                new { id = member.id },
                member);
        }

        // PUT: api/members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(
            int id,
            UpdateMembersDTO dto)
        {
            var updated = await _membersService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var deleted = await _membersService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}