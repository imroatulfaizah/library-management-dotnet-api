namespace LibraryManagementAPI.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiredAt { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
