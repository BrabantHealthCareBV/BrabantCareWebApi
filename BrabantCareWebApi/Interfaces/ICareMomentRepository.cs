using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface ICareMomentRepository<T>
    {
        Task<CareMoment> InsertAsync(CareMoment careMoment);
        Task<CareMoment?> ReadAsync(Guid id);
        Task<IEnumerable<CareMoment>> ReadAsync();
        Task UpdateAsync(CareMoment careMoment);
        Task DeleteAsync(Guid id);
    }
}
