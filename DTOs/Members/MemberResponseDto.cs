namespace LibraryManagementAPI.DTOs.Members
{
    public class MemberResponseDto
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
