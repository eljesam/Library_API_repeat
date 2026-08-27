using Library_API_repeat.Api.DTOs.Loans;

namespace Library_API_repeat.Api.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDTO>> GetAllLoansAsync();
        Task<IEnumerable<LoanDTO>> GetByUserIdAsync(int userId);

        Task<LoanDTO?> GetByIdAsync(int id);

        Task<LoanDTO?> CreateAsync(CreateLoanDTO dto);

        Task<bool> UpdateAsync(int id, UpdateLoanDTO dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> BookExistsAsync(int bookId);

        Task<bool> MemberExistsAsync(int memberId);
    }
}
