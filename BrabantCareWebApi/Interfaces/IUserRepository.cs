using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<List<User>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
    }
}
