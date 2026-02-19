using LibraryManagementAPI.DTOs.Common;
using LibraryManagementAPI.DTOs.Members;
using LibraryManagementAPI.Responses;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _service;

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await _service.GetPagedAsync(pageNumber, pageSize, search);
            return Ok(ApiResponse<PagedResult<MemberResponseDto>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<MemberResponseDto>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberRequestDto request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(ApiResponse<MemberResponseDto>.Ok(result, "Member created successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("", "Member deleted successfully"));
        }
    }
}
