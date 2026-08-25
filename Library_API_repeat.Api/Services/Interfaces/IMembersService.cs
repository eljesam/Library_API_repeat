using Library_API_repeat.Api.DTOs.Members;
namespace Library_API_repeat.Api.Services.Interfaces
{
    public interface IMembersService
    {
        Task<IEnumerable<MembersDTO>> GetAllAsync();

        Task<MembersDTO?> GetByIdAsync(int id);

        Task<MembersDTO> CreateAsync(CreateMembersDTO dto);

        Task<bool> UpdateAsync(int id, UpdateMembersDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
