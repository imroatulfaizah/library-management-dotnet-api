using LibraryManagementAPI.DTOs.Books;
using LibraryManagementAPI.DTOs.Common;
using LibraryManagementAPI.Responses;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // secure all endpoints
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationQuery query)
        {
            var result = await _service.GetPagedAsync(query);
            return Ok(ApiResponse<PagedResult<BookResponseDto>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<BookResponseDto>.Ok(result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookRequestDto request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(ApiResponse<BookResponseDto>.Ok(result, "Book created successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookRequestDto request)
        {
            var result = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<BookResponseDto>.Ok(result, "Book updated successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("", "Book deleted successfully"));
        }
    }
}
