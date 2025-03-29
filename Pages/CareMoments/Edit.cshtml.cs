using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class EditModel : PageModel
    {
        private readonly CareMomentRepository _careMomentRepository;

        public EditModel(CareMomentRepository repository)
        {
            _careMomentRepository = repository;
        }

        [BindProperty]
        public CareMoment UpdatedCareMoment { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var moment = await _careMomentRepository.ReadAsync(id);
            if (moment == null) return NotFound();
            UpdatedCareMoment = moment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _careMomentRepository.UpdateAsync(UpdatedCareMoment);
            return RedirectToPage("Index");
        }
    }
}
