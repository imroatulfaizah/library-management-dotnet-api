using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs.Loans;
using LibraryManagementAPI.Entities;
using LibraryManagementAPI.Repositories;
using LibraryManagementAPI.Responses;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDbContext _context;
        private readonly ILoanRepository _loanRepository;

        public LoanService(LibraryDbContext context, ILoanRepository loanRepository)
        {
            _context = context;
            _loanRepository = loanRepository;
        }

        public async Task<List<LoanResponseDto>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();

            return loans.Select(x => new LoanResponseDto
            {
                LoanId = x.LoanId,
                BookId = x.BookId,
                BookTitle = x.Book?.Title ?? "",
                MemberId = x.MemberId,
                MemberName = x.Member?.FullName ?? "",
                LoanDate = x.LoanDate,
                DueDate = x.DueDate,
                ReturnDate = x.ReturnDate
            }).ToList();
        }

        public async Task<LoanResponseDto> BorrowAsync(BorrowBookRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var book = await _context.Books.FirstOrDefaultAsync(x => x.BookId == request.BookId);
            if (book == null)
                throw new AppException("Book not found", 404);

            var member = await _context.Members.FirstOrDefaultAsync(x => x.MemberId == request.MemberId);
            if (member == null)
                throw new AppException("Member not found", 404);

            if (book.CopiesAvailable <= 0)
                throw new AppException("Book is out of stock", 400);

            var activeLoan = await _loanRepository.GetActiveLoanAsync(request.BookId, request.MemberId);
            if (activeLoan != null)
                throw new AppException("Member already borrowed this book and has not returned it", 400);

            var loan = new Loan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(request.LoanDays),
                CreatedAt = DateTime.UtcNow
            };

            await _loanRepository.AddAsync(loan);

            // decrement stock
            book.CopiesAvailable -= 1;
            _context.Books.Update(book);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var createdLoan = await _loanRepository.GetByIdAsync(loan.LoanId);

            return new LoanResponseDto
            {
                LoanId = createdLoan!.LoanId,
                BookId = createdLoan.BookId,
                BookTitle = createdLoan.Book?.Title ?? "",
                MemberId = createdLoan.MemberId,
                MemberName = createdLoan.Member?.FullName ?? "",
                LoanDate = createdLoan.LoanDate,
                DueDate = createdLoan.DueDate,
                ReturnDate = createdLoan.ReturnDate
            };
        }

        public async Task<LoanResponseDto> ReturnAsync(ReturnBookRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var loan = await _loanRepository.GetByIdAsync(request.LoanId);
            if (loan == null)
                throw new AppException("Loan not found", 404);

            if (loan.ReturnDate != null)
                throw new AppException("Book already returned", 400);

            loan.ReturnDate = DateTime.UtcNow;

            var book = await _context.Books.FirstOrDefaultAsync(x => x.BookId == loan.BookId);
            if (book == null)
                throw new AppException("Book not found", 404);

            book.CopiesAvailable += 1;

            _context.Books.Update(book);
            _loanRepository.Update(loan);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var updatedLoan = await _loanRepository.GetByIdAsync(loan.LoanId);

            return new LoanResponseDto
            {
                LoanId = updatedLoan!.LoanId,
                BookId = updatedLoan.BookId,
                BookTitle = updatedLoan.Book?.Title ?? "",
                MemberId = updatedLoan.MemberId,
                MemberName = updatedLoan.Member?.FullName ?? "",
                LoanDate = updatedLoan.LoanDate,
                DueDate = updatedLoan.DueDate,
                ReturnDate = updatedLoan.ReturnDate
            };
        }
    }
}
