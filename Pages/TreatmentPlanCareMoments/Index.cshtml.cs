using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BrabantCareWebApi.Pages.TreatmentPlanCareMoments
{
    public class IndexModel : PageModel
    {
        private readonly TreatmentPlanCareMomentRepository _repository;

        public IndexModel(TreatmentPlanCareMomentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TreatmentPlanCareMoment> CareMoments { get; set; } = new List<TreatmentPlanCareMoment>();

        [BindProperty]
        public Guid TreatmentPlanID { get; set; }

        public async Task OnGetAsync()
        {
            CareMoments = await _repository.ReadAsync(TreatmentPlanID);
        }
    }
}
