using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

public class ManageModel : PageModel
{
    private readonly TreatmentPlanCareMomentRepository _tpCareMomentRepo;
    private readonly CareMomentRepository _careMomentRepo;

    public ManageModel(TreatmentPlanCareMomentRepository tpCareMomentRepo, CareMomentRepository careMomentRepo)
    {
        _tpCareMomentRepo = tpCareMomentRepo;
        _careMomentRepo = careMomentRepo;
    }

    [BindProperty(SupportsGet = true)]
    public Guid TreatmentPlanID { get; set; }

    public IEnumerable<CareMoment> AvailableCareMoments { get; set; } = new List<CareMoment>();
    public IEnumerable<TreatmentPlanCareMoment> LinkedCareMoments { get; set; } = new List<TreatmentPlanCareMoment>();

    public async Task OnGetAsync()
    {
        LinkedCareMoments = await _tpCareMomentRepo.ReadAsync(TreatmentPlanID);
        AvailableCareMoments = await _careMomentRepo.ReadAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(Guid careMomentId)
    {
        var newLink = new TreatmentPlanCareMoment
        {
            TreatmentPlanID = TreatmentPlanID,
            CareMomentID = careMomentId,
            Order = LinkedCareMoments.Count() + 1
        };

        await _tpCareMomentRepo.InsertAsync(newLink);
        return RedirectToPage(new { TreatmentPlanID });
    }

    public async Task<IActionResult> OnPostRemoveAsync(Guid careMomentId)
    {
        await _tpCareMomentRepo.DeleteAsync(TreatmentPlanID, careMomentId);
        return RedirectToPage(new { TreatmentPlanID });
    }
}
