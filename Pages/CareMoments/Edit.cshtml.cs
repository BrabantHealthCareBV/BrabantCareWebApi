using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class EditModel : PageModel
    {
        private readonly CareMomentRepository _repository;

        public EditModel(CareMomentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public CareMoment UpdatedCareMoment { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var moment = await _repository.ReadAsync(id);
            if (moment == null) return NotFound();
            UpdatedCareMoment = moment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _repository.UpdateAsync(UpdatedCareMoment);
            return RedirectToPage("Index");
        }
    }
}
