using LibraryManagementAPI.DTOs.Books;
using LibraryManagementAPI.DTOs.Common;

namespace LibraryManagementAPI.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookResponseDto>> GetPagedAsync(PaginationQuery query);

        Task<BookResponseDto> GetByIdAsync(int id);
        Task<BookResponseDto> CreateAsync(CreateBookRequestDto request);
        Task<BookResponseDto> UpdateAsync(int id, UpdateBookRequestDto request);
        Task DeleteAsync(int id);
    }
}
