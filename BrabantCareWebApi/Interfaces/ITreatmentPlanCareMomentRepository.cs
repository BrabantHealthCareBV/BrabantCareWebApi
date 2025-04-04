using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface ITreatmentPlanCareMomentRepository<T>
    {
        Task InsertAsync(TreatmentPlanCareMoment entity);
        Task<IEnumerable<TreatmentPlanCareMoment>> ReadAsync(Guid treatmentPlanId);
        Task UpdateAsync(TreatmentPlanCareMoment entity);
        Task DeleteAsync(Guid treatmentPlanId, Guid careMomentId);
    }
}
