using LibraryManagementAPI.Entities;

namespace LibraryManagementAPI.Repositories
{
    public interface IMemberRepository
    {
        Task<(List<Member> Items, int TotalItems)> GetPagedAsync(int pageNumber, int pageSize, string? search);
        Task<Member?> GetByIdAsync(int id);
        Task<Member> CreateAsync(Member member);
        Task DeleteAsync(Member member);
    }
}
