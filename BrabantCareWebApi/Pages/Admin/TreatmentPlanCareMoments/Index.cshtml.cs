using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.TreatmentPlanCareMoments;
public class IndexModel : PageModel
{
    private readonly TreatmentPlanRepository _treatmentPlanRepo;

    public IndexModel(TreatmentPlanRepository treatmentPlanRepo)
    {
        _treatmentPlanRepo = treatmentPlanRepo;
    }

    public IEnumerable<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();

    public async Task OnGetAsync()
    {
        TreatmentPlans = await _treatmentPlanRepo.ReadAsync();
    }
}
