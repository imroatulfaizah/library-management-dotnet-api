namespace LibraryManagementAPI.DTOs.Loans
{
    public class LoanResponseDto
    {
        public int LoanId { get; set; }

        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;

        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate != null;
    }
}
