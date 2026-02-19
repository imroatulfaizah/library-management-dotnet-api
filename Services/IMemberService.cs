using LibraryManagementAPI.DTOs.Common;
using LibraryManagementAPI.DTOs.Members;
using LibraryManagementAPI.Responses;

namespace LibraryManagementAPI.Services
{
    public interface IMemberService
    {
        Task<PagedResult<MemberResponseDto>> GetPagedAsync(int pageNumber, int pageSize, string? search);
        Task<MemberResponseDto> GetByIdAsync(int id);
        Task<MemberResponseDto> CreateAsync(CreateMemberRequestDto request);
        Task DeleteAsync(int id);
    }
}
