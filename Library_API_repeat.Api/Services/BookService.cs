using Library_API_repeat.Api.Data;
using Library_API_repeat.Api.DTOs.Books;
using Library_API_repeat.Api.Models;
using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API_repeat.Api.Services
{
    public class BookService: IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync() {

            return await _context.Books
                .Include(b => b.Author)
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    PublicationYear = book.PublicationYear,
                    IsAvailable = book.IsAvailable,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author != null ? book.Author.name : string.Empty
                })
                .ToListAsync();
    }
        public async Task<BookDTO?> GetByIdAsync(int id)
        {

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.name ?? string.Empty
            };
        }
            public async Task<BookDTO?> CreateAsync(CreateBookDTO dto)
        {
            var author = await _context.Authors.FindAsync(dto.AuthorId);

            if (author == null)
            {
                return null;
            }

            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                PublicationYear = dto.PublicationYear,
                IsAvailable = dto.IsAvailable,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable,
                AuthorId = book.AuthorId,
                AuthorName = author.name
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDTO dto)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
            book.PublicationYear = dto.PublicationYear;
            book.IsAvailable = dto.IsAvailable;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _context.Authors
                .AnyAsync(a => a.id == authorId);
        }
    }
}

