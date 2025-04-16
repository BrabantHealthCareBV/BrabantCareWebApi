using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;
using System.Linq;

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

    public List<TreatmentPlanCareMoment> LinkedCareMoments { get; set; } = new();
    public IEnumerable<CareMoment> AvailableCareMoments { get; set; } = new List<CareMoment>();

    public async Task OnGetAsync()
    {
        var linked = await _tpCareMomentRepo.ReadAsync(TreatmentPlanID);
        var allCareMoments = await _careMomentRepo.ReadAsync();

        LinkedCareMoments = linked.Select(l => new TreatmentPlanCareMoment
        {
            CareMomentID = l.CareMomentID,
            TreatmentPlanID = l.TreatmentPlanID,
            Order = l.Order,
            CareMomentName = allCareMoments.FirstOrDefault(c => c.ID == l.CareMomentID)?.Name ?? "Unknown"
        }).ToList();

        AvailableCareMoments = allCareMoments;
    }

    public async Task<IActionResult> OnPostAddAsync(Guid TreatmentPlanID, Guid careMomentId)
    {
        var newLink = new TreatmentPlanCareMoment
        {
            TreatmentPlanID = TreatmentPlanID,
            CareMomentID = careMomentId,
            Order = LinkedCareMoments.Count + 1
        };

        await _tpCareMomentRepo.InsertAsync(newLink);
        return RedirectToPage(new { TreatmentPlanID });
    }

    public async Task<IActionResult> OnPostRemoveAsync(Guid TreatmentPlanID, Guid careMomentId)
    {
        await _tpCareMomentRepo.DeleteAsync(TreatmentPlanID, careMomentId);
        return RedirectToPage(new { TreatmentPlanID });
    }

    public async Task<IActionResult> OnPostUpdateOrderAsync(Guid treatmentPlanID, Guid careMomentId, int newOrder)
    {
        var updatedEntity = new TreatmentPlanCareMoment
        {
            TreatmentPlanID = treatmentPlanID,
            CareMomentID = careMomentId,
            Order = newOrder
        };

        await _tpCareMomentRepo.UpdateAsync(updatedEntity);
        return RedirectToPage(new { TreatmentPlanID });
    }

}

