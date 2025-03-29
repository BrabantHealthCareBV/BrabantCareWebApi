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
        public TreatmentPlan TreatmentPlanToDelete { get; set; } = new();
        public DeleteModel(TreatmentPlanRepository treatmentPlanRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TreatmentPlanToDelete = await _treatmentPlanRepository.ReadAsync(id);
            if (TreatmentPlanToDelete == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _treatmentPlanRepository.DeleteAsync(TreatmentPlanToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
