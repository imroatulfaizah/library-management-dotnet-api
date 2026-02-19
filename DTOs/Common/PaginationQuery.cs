namespace LibraryManagementAPI.DTOs.Common
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
        public string? SortBy { get; set; } = "title";
        public string? SortOrder { get; set; } = "asc";
    }
}
