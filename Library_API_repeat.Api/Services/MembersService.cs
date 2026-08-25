using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Members;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
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
            .Select(Member => new MembersDTO{
            
                id = Member.id,
                Name = Member.Name,
                Email = Member.Email,
                MembershipDate = Member.MembershipDate

        }).ToListAsync();
        }
       

        public async Task<MembersDTO?> GetByIdAsync(int id)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.id == id);

            if (member == null)
            {
                return null;
            }

            return new MembersDTO
            {
                id = member.id,
                Name = member.Name,
                Email = member.Email,
                MembershipDate = member.MembershipDate
            };
        }

        public async Task<MembersDTO> CreateAsync(CreateMembersDTO dto)
        {
            var member = new Member
            {
                Name = dto.Name,
                Email = dto.Email,
                MembershipDate = dto.MembershipDate
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return new MembersDTO
            {
                id = member.id,
                Name = member.Name,
                Email = member.Email,
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

            member.Name = dto.Name;
            member.Email = dto.Email;
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
