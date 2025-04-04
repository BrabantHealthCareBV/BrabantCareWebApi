using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface IDoctorRepository<T>
    {
        Task<Doctor> InsertAsync(Doctor doctor);
        Task<Doctor?> ReadAsync(Guid id);
        Task<IEnumerable<Doctor>> ReadAsync();
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(Guid id);
    }
}
