using System.Security.Claims;
using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Loans;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // Get: api/loans
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllLoansAsync();

            return Ok(loans);
        }
        // GET: api/loans/my
        // Logged-in members can only view their own loans
        [HttpGet("my")]
        public async Task<IActionResult> GetMyLoans()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var loans =
                await _loanService.GetByUserIdAsync(userId);

            return Ok(loans);
        }


        //Get: api/loans/5

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan =
                await _loanService.GetByIdAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }



        //POST: api/loans
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateLoanDTO dto)
        {
            var loan = await _loanService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = loan.id },
                loan);
        }


        //PUT: api/loans/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            UpdateLoanDTO dto)
        {
            var updated =
                await _loanService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        //Delete: api/loans/5

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _loanService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
    }
    

