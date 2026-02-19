namespace LibraryManagementAPI.DTOs.Members
{
    public class CreateMemberRequestDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
