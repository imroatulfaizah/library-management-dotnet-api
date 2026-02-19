namespace LibraryManagementAPI.DTOs.Books
{
    public class BookResponseDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public int? PublishedYear { get; set; }
        public int CopiesTotal { get; set; }
        public int CopiesAvailable { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
    }
}
