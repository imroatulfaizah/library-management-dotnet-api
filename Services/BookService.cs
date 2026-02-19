using LibraryManagementAPI.DTOs.Books;
using LibraryManagementAPI.DTOs.Common;
using LibraryManagementAPI.Entities;
using LibraryManagementAPI.Repositories;
using LibraryManagementAPI.Responses;

namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PagedResult<BookResponseDto>> GetPagedAsync(PaginationQuery query)
        {
            if (query.PageNumber <= 0) query.PageNumber = 1;
            if (query.PageSize <= 0) query.PageSize = 10;
            if (query.PageSize > 100) query.PageSize = 100;

            var (items, totalItems) = await _bookRepository.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.Search,
                query.SortBy,
                query.SortOrder
            );

            var mapped = items.Select(x => new BookResponseDto
            {
                BookId = x.BookId,
                Title = x.Title,
                Isbn = x.ISBN,
                PublishedYear = x.PublishedYear,
                CopiesTotal = x.CopiesTotal,
                CopiesAvailable = x.CopiesAvailable,
                AuthorId = x.AuthorId,
                AuthorName = x.Author?.FullName ?? ""
            }).ToList();

            int totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            return new PagedResult<BookResponseDto>
            {
                Items = mapped,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        // sisanya tetap sama
        public async Task<BookResponseDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                throw new AppException("Book not found", 404);

            return new BookResponseDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Isbn = book.ISBN,
                PublishedYear = book.PublishedYear,
                CopiesTotal = book.CopiesTotal,
                CopiesAvailable = book.CopiesAvailable,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.FullName ?? ""
            };
        }

        public async Task<BookResponseDto> CreateAsync(CreateBookRequestDto request)
        {
            var existing = await _bookRepository.GetByIsbnAsync(request.Isbn);
            if (existing != null)
                throw new AppException("ISBN already exists", 400);

            var book = new Book
            {
                Title = request.Title,
                ISBN = request.Isbn,
                PublishedYear = request.PublishedYear,
                CopiesTotal = request.CopiesTotal,
                CopiesAvailable = request.CopiesTotal,
                AuthorId = request.AuthorId,
                CreatedAt = DateTime.UtcNow
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            var created = await _bookRepository.GetByIdAsync(book.BookId);

            return new BookResponseDto
            {
                BookId = created!.BookId,
                Title = created.Title,
                Isbn = created.ISBN,
                PublishedYear = created.PublishedYear,
                CopiesTotal = created.CopiesTotal,
                CopiesAvailable = created.CopiesAvailable,
                AuthorId = created.AuthorId,
                AuthorName = created.Author?.FullName ?? ""
            };
        }

        public async Task<BookResponseDto> UpdateAsync(int id, UpdateBookRequestDto request)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new AppException("Book not found", 404);

            int borrowed = book.CopiesTotal - book.CopiesAvailable;
            if (request.CopiesTotal < borrowed)
                throw new AppException($"CopiesTotal cannot be less than borrowed copies ({borrowed})", 400);

            book.Title = request.Title;
            book.PublishedYear = request.PublishedYear;
            book.AuthorId = request.AuthorId;
            book.CopiesTotal = request.CopiesTotal;
            book.CopiesAvailable = request.CopiesTotal - borrowed;
            book.UpdatedAt = DateTime.UtcNow;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();

            var updated = await _bookRepository.GetByIdAsync(book.BookId);

            return new BookResponseDto
            {
                BookId = updated!.BookId,
                Title = updated.Title,
                Isbn = updated.ISBN,
                PublishedYear = updated.PublishedYear,
                CopiesTotal = updated.CopiesTotal,
                CopiesAvailable = updated.CopiesAvailable,
                AuthorId = updated.AuthorId,
                AuthorName = updated.Author?.FullName ?? ""
            };
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new AppException("Book not found", 404);

            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
        }
    }
}
