using Library_API_repeat.Api.DTOs.Books;

namespace Library_API_repeat.Api.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();

        Task<BookDTO?> GetByIdAsync(int id);

        Task<BookDTO?> CreateAsync(CreateBookDTO dto);

        Task<bool> UpdateAsync(int id, UpdateBookDTO dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> AuthorExistsAsync(int authorId);
    }
}
