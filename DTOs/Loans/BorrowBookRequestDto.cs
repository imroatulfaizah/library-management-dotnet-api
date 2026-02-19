namespace LibraryManagementAPI.DTOs.Loans
{
    public class BorrowBookRequestDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int LoanDays { get; set; } = 7;
    }
}
