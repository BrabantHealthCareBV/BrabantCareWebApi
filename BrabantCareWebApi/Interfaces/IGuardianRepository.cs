using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface IGuardianRepository<T>
    {
        Task<Guardian> InsertAsync(Guardian guardian);
        Task<Guardian?> ReadAsync(Guid id);
        Task<IEnumerable<Guardian>> ReadAsync();
        Task<IEnumerable<Guardian>> ReadByUserAsync(string userId);
        Task UpdateAsync(Guardian guardian);
        Task DeleteAsync(Guid id);
    }
}
