using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Authentication;
using Library_API_repeat.Api.DTOs.Members;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
using Library_API_repeat.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Library_API_repeat.Api.Services
{
    public class MembersService: IMembersService
    {
        private readonly LibraryDbContext _context;

        public MembersService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MembersDTO>> GetAllAsync()
        {
            return await _context.Members
             .Include(m => m.User)
             .Select(member => new MembersDTO
             {
                 id = member.id,
                 UserId = member.UserId,
                 Name = member.User != null
                     ? member.User.Name
                     : string.Empty,
                 Email = member.User != null
                     ? member.User.Email
                     : string.Empty,
                 MembershipDate = member.MembershipDate
             })
             .ToListAsync();
        }
       

        public async Task<MembersDTO?> GetByIdAsync(int id)
        {
            var member = await _context.Members
        .Include(m => m.User)
        .FirstOrDefaultAsync(m => m.id == id);

            if (member == null)
            {
                return null;
            }

            return new MembersDTO
            {
                id = member.id,
                UserId = member.UserId,
                Name = member.User?.Name ?? string.Empty,
                Email = member.User?.Email ?? string.Empty,
                MembershipDate = member.MembershipDate
            };
        }

        public async Task<MembersDTO> CreateAsync(CreateMembersDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }

            var existingMember = await _context.Members
                .AnyAsync(m => m.UserId == dto.UserId);

            if (existingMember)
            {
                throw new InvalidOperationException(
                    "This user already has a membership.");
            }

            var member = new Member
            {
                UserId = dto.UserId,
                MembershipDate = dto.MembershipDate
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return new MembersDTO
            {
                id = member.id,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                MembershipDate = member.MembershipDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateMembersDTO dto)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return false;
            }

            member.MembershipDate = dto.MembershipDate;

            await _context.SaveChangesAsync();

            return true;
        }
    

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return false;
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
