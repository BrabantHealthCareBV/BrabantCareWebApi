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
        private readonly CareMomentRepository _careMomentRepository;

        [BindProperty]
        public TreatmentPlan TreatmentPlan { get; set; } = new();

        public List<SelectListItem> CareMomentSelectList { get; set; } = new();

        public EditModel(TreatmentPlanRepository treatmentPlanRepository, CareMomentRepository careMomentRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
            _careMomentRepository = careMomentRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TreatmentPlan = await _treatmentPlanRepository.ReadAsync(id);
            if (TreatmentPlan == null) return NotFound();
            var careMoments = await _careMomentRepository.ReadAsync();
            CareMomentSelectList = careMoments
                .Select(cm => new SelectListItem { Value = cm.ID.ToString(), Text = cm.Name })
                .ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _treatmentPlanRepository.UpdateAsync(TreatmentPlan);
            return RedirectToPage("Index");
        }
    }
}
