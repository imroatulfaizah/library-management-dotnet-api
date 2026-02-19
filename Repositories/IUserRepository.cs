using LibraryManagementAPI.Entities;
using LibraryManagementAPI.Models.Entities;

namespace LibraryManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<Users?> GetByUsernameAsync(string username);
        Task AddAsync(Users user);
        Task SaveChangesAsync();
    }
}
