using LibraryManagementAPI.DTOs.Loans;

namespace LibraryManagementAPI.Services
{
    public interface ILoanService
    {
        Task<List<LoanResponseDto>> GetAllAsync();
        Task<LoanResponseDto> BorrowAsync(BorrowBookRequestDto request);
        Task<LoanResponseDto> ReturnAsync(ReturnBookRequestDto request);
    }
}
