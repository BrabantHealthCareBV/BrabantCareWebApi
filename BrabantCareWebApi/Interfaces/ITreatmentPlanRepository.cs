using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Interfaces
{
    public interface ITreatmentPlanRepository<T>
    {
        Task<TreatmentPlan> InsertAsync(TreatmentPlan treatmentPlan);
        Task<TreatmentPlan?> ReadAsync(Guid id);
        Task<IEnumerable<TreatmentPlan>> ReadAsync();
        Task UpdateAsync(TreatmentPlan treatmentPlan);
        Task DeleteAsync(Guid id);


    }
}
