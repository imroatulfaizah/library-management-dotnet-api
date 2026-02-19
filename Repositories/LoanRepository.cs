using LibraryManagementAPI.Data;
using LibraryManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(x => x.Book)
                .Include(x => x.Member)
                .OrderByDescending(x => x.LoanDate)
                .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(x => x.Book)
                .Include(x => x.Member)
                .FirstOrDefaultAsync(x => x.LoanId == id);
        }

        public async Task<Loan?> GetActiveLoanAsync(int bookId, int memberId)
        {
            return await _context.Loans
                .FirstOrDefaultAsync(x =>
                    x.BookId == bookId &&
                    x.MemberId == memberId &&
                    x.ReturnDate == null);
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
