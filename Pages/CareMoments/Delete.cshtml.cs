using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class DeleteModel : PageModel
    {
        private readonly CareMomentRepository _careMomentRepository;

        public DeleteModel(CareMomentRepository repository)
        {
            _careMomentRepository = repository;
        }

        [BindProperty]
        public CareMoment CareMomentToDelete { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var careMoment = await _careMomentRepository.ReadAsync(id);
            if (careMoment == null) return NotFound();
            CareMomentToDelete = careMoment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _careMomentRepository.DeleteAsync(CareMomentToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
