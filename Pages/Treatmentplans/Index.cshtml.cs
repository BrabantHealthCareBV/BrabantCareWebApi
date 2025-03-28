using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;

namespace BrabantCareWebApi.Pages.TreatmentPlans
{
    public class IndexModel : PageModel
    {
        private readonly TreatmentPlanRepository _treatmentPlanRepository;

        public IndexModel(TreatmentPlanRepository treatmentPlanRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public List<TreatmentPlan> TreatmentPlans { get; set; } = new();

        public async Task OnGetAsync()
        {
            TreatmentPlans = (await _treatmentPlanRepository.ReadAsync()).ToList();
        }
    }
}
