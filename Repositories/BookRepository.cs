using LibraryManagementAPI.Data;
using LibraryManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Book> Items, int TotalItems)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? search,
            string? sortBy,
            string? sortOrder)
        {
            var query = _context.Books
                .Include(x => x.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(x =>
                    x.Title.ToLower().Contains(search) ||
                    x.ISBN.ToLower().Contains(search) ||
                    x.Author.FullName.ToLower().Contains(search)
                );
            }

            bool isDesc = sortOrder?.ToLower() == "desc";

            query = sortBy?.ToLower() switch
            {
                "title" => isDesc ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                "isbn" => isDesc ? query.OrderByDescending(x => x.ISBN) : query.OrderBy(x => x.ISBN),
                "year" => isDesc ? query.OrderByDescending(x => x.PublishedYear) : query.OrderBy(x => x.PublishedYear),
                "available" => isDesc ? query.OrderByDescending(x => x.CopiesAvailable) : query.OrderBy(x => x.CopiesAvailable),
                _ => query.OrderBy(x => x.Title)
            };

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.BookId == id);
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.ISBN == isbn);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
