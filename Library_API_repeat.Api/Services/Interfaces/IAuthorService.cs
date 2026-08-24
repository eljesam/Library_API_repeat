using Library_API_repeat.Api.DTOs.Authors;
namespace Library_API_repeat.Api.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAsync();
        Task<AuthorDTO?> GetByIdAsync(int id);
        Task<AuthorDTO> CreateAsync(CreateAuthorDTO dto);
        Task<bool> UpdateAsync(int id, UpdateAuthorDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
