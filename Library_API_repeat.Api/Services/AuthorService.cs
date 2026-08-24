using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Authors;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDTO>>GetAllAsync()
        {
            return await _context.Authors
                .Select(author => new AuthorDTO
                {
                    Id = author.id,
                    Name = author.name,
                    Country = author.Country

                })
                .ToListAsync();
        }

        public async Task<AuthorDTO> GetByIdAsync(int id)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.id == id);

            if (author == null)
            {
                return null;
            }

            return new AuthorDTO
            {
                Id = author.id,
                Name = author.name,
                Country = author.Country
            };

        }
    }
}
