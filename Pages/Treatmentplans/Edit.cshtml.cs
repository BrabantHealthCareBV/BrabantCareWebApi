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
        public TreatmentPlan updatedTreatmentPlan { get; set; } = new();
        public EditModel(TreatmentPlanRepository treatmentPlanRepository, CareMomentRepository careMomentRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            updatedTreatmentPlan = await _treatmentPlanRepository.ReadAsync(id);
            if (updatedTreatmentPlan == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _treatmentPlanRepository.UpdateAsync(updatedTreatmentPlan);
            return RedirectToPage("Index");
        }
    }
}
