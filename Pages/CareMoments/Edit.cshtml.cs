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
        public CareMoment updatedCareMoment { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            updatedCareMoment = await _careMomentRepository.ReadAsync(id);
            if (updatedCareMoment == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _careMomentRepository.UpdateAsync(updatedCareMoment);
            return RedirectToPage("Index");
        }
    }
}
