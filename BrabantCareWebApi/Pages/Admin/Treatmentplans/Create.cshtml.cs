using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrabantCareWebApi.Pages.TreatmentPlans
{
    public class CreateModel : PageModel
    {
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly CareMomentRepository _careMomentRepository;

        [BindProperty]
        public TreatmentPlan NewTreatmentPlan { get; set; } = new();


        public CreateModel(TreatmentPlanRepository treatmentPlanRepository)
        {
            _treatmentPlanRepository = treatmentPlanRepository;
        }

        public async Task OnGetAsync()
        {
            NewTreatmentPlan = new TreatmentPlan();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log errors
                }
                return Page(); // Return the form with validation errors
            }

            NewTreatmentPlan.ID = Guid.NewGuid();
            await _treatmentPlanRepository.InsertAsync(NewTreatmentPlan);
            return RedirectToPage("Index");
        }
    }
}
