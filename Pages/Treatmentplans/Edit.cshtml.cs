using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrabantCareWebApi.Pages.TreatmentPlans
{
    public class EditModel : PageModel
    {
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        [BindProperty]
        public TreatmentPlan UpdatedTreatmentPlan { get; set; } = new();
        public EditModel(TreatmentPlanRepository treatmentPlanRepository, CareMomentRepository careMomentRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            UpdatedTreatmentPlan = await _treatmentPlanRepository.ReadAsync(id);
            if (UpdatedTreatmentPlan == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _treatmentPlanRepository.UpdateAsync(UpdatedTreatmentPlan);
            return RedirectToPage("Index");
        }
    }
}
