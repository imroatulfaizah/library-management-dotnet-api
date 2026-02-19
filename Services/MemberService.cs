using LibraryManagementAPI.DTOs.Common;
using LibraryManagementAPI.DTOs.Members;
using LibraryManagementAPI.Entities;
using LibraryManagementAPI.Repositories;

namespace LibraryManagementAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<MemberResponseDto>> GetPagedAsync(int pageNumber, int pageSize, string? search)
        {
            var (items, totalItems) = await _repository.GetPagedAsync(pageNumber, pageSize, search);

            var result = new PagedResult<MemberResponseDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items.Select(x => new MemberResponseDto
                {
                    MemberId = x.MemberId,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    CreatedAt = x.CreatedAt
                }).ToList()
            };

            return result;
        }

        public async Task<MemberResponseDto> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                throw new Exception("Member not found");

            return new MemberResponseDto
            {
                MemberId = member.MemberId,
                FullName = member.FullName,
                Email = member.Email,
                Phone = member.Phone,
                CreatedAt = member.CreatedAt
            };
        }

        public async Task<MemberResponseDto> CreateAsync(CreateMemberRequestDto request)
        {
            var entity = new Member
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);

            return new MemberResponseDto
            {
                MemberId = created.MemberId,
                FullName = created.FullName,
                Email = created.Email,
                Phone = created.Phone,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                throw new Exception("Member not found");

            var trackedMember = new Member { MemberId = id };
            await _repository.DeleteAsync(trackedMember);
        }
    }
}
