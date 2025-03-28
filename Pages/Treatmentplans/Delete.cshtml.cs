using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.TreatmentPlans
{
    public class DeleteModel : PageModel
    {
        private readonly TreatmentPlanRepository _treatmentPlanRepository;

        [BindProperty]
        public TreatmentPlan TreatmentPlan { get; set; } = new();

        public DeleteModel(TreatmentPlanRepository treatmentPlanRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public async Task OnGetAsync(Guid id)
        {
            TreatmentPlan = await _treatmentPlanRepository.ReadAsync(id) ?? new TreatmentPlan();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (TreatmentPlan != null)
            {
                await _treatmentPlanRepository.DeleteAsync(TreatmentPlan.ID);
            }

            return RedirectToPage("Index");
        }
    }
}
