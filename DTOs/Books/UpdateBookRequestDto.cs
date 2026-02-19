namespace LibraryManagementAPI.DTOs.Books
{
    public class UpdateBookRequestDto
    {
        public string Title { get; set; } = null!;
        public int? PublishedYear { get; set; }
        public int CopiesTotal { get; set; }
        public int AuthorId { get; set; }
    }
}
