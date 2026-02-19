using LibraryManagementAPI.Entities;

namespace LibraryManagementAPI.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<Loan?> GetActiveLoanAsync(int bookId, int memberId);
        Task AddAsync(Loan loan);
        void Update(Loan loan);
        Task SaveChangesAsync();
    }
}
