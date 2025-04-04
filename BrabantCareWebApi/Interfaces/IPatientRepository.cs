using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface IPatientRepository<T>
    {
        Task<Patient> InsertAsync(Patient patient);
        Task<Patient?> ReadAsync(Guid id);
        Task<IEnumerable<Patient>> ReadAsync();
        Task<IEnumerable<Patient>> ReadByUserAsync(string userId);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Guid id);
    }
}
