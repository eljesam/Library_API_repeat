using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Loans;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Services
{
    public class LoanService: ILoanService
    {
        private readonly LibraryDbContext _context;

        public LoanService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LoanDTO>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                  .ThenInclude(m => m!.User)
                .Select(loan => new LoanDTO
                {
                    id = loan.id,
                    BookId = loan.BookId,
                    BookTitle = loan.Book != null
                        ? loan.Book.Title
                        : string.Empty,
                    MemberId = loan.MemberId,
                    MemberName = loan.Member != null && loan.Member.User != null
                                         ? loan.Member.User.Name
                                                      : string.Empty,
                    LoanDate = loan.LoanDate,
                    DueDate = loan.DueDate,
                    ReturnDate = loan.ReturnDate
                })
                .ToListAsync();
        }

        public async Task<LoanDTO?> GetByIdAsync(int id)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                  .ThenInclude(m => m!.User)
                .FirstOrDefaultAsync(l => l.id == id);

            if (loan == null)
            {
                return null;
            }

            return new LoanDTO
            {
                id = loan.id,
                BookId = loan.BookId,
                BookTitle = loan.Book?.Title ?? string.Empty,
                MemberId = loan.MemberId,
                MemberName = loan.Member?.User?.Name ?? string.Empty,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate
            };
        }

        public async Task<LoanDTO?> CreateAsync(CreateLoanDTO dto)
        {
            var book = await _context.Books.FindAsync(dto.BookId);

            if (book == null)
            {
                return null;
            }

            var member = await _context.Members
                      .Include(m => m.User)
                          .FirstOrDefaultAsync(m => m.id == dto.MemberId); 

            if (member == null)
            {
                return null;
            }

            var loan = new Loan
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = dto.LoanDate,
                DueDate = dto.DueDate,
                ReturnDate = dto.ReturnDate
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return new LoanDTO
            {
                id = loan.id,
                BookId = loan.BookId,
                BookTitle = book.Title,
                MemberId = loan.MemberId,
                MemberName = member.User?.Name ?? string.Empty,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateLoanDTO dto)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return false;
            }

            loan.BookId = dto.BookId;
            loan.MemberId = dto.MemberId;
            loan.LoanDate = dto.LoanDate;
            loan.DueDate = dto.DueDate;
            loan.ReturnDate = dto.ReturnDate;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return false;
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _context.Books
               .AnyAsync(b => b.Id == bookId);
        }

        public async Task<bool> MemberExistsAsync(int memberId)
        {
            return await _context.Members
               .AnyAsync(m => m.id == memberId);
        }
    }
}
