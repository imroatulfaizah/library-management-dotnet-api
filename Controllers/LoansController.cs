using LibraryManagementAPI.DTOs.Loans;
using LibraryManagementAPI.Responses;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoansController(ILoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(ApiResponse<List<LoanResponseDto>>.Ok(result));
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow(BorrowBookRequestDto request)
        {
            var result = await _service.BorrowAsync(request);
            return Ok(ApiResponse<LoanResponseDto>.Ok(result, "Book borrowed successfully"));
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return(ReturnBookRequestDto request)
        {
            var result = await _service.ReturnAsync(request);
            return Ok(ApiResponse<LoanResponseDto>.Ok(result, "Book returned successfully"));
        }
    }
}
