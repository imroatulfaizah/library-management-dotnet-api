using LibraryManagementAPI.Entities;

namespace LibraryManagementAPI.Repositories
{
    public interface IBookRepository
    {
        Task<(List<Book> Items, int TotalItems)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? search,
            string? sortBy,
            string? sortOrder
        );

        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIsbnAsync(string isbn);

        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task SaveChangesAsync();
    }
}
