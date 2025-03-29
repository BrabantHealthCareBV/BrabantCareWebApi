using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class DeleteModel : PageModel
    {
        private readonly CareMomentRepository _careMomentRepository;
        [BindProperty]
        public CareMoment CareMomentToDelete { get; set; } = new();
        public DeleteModel(CareMomentRepository repository)
        {
            _careMomentRepository = repository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            CareMomentToDelete = await _careMomentRepository.ReadAsync(id);
            if (CareMomentToDelete == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _careMomentRepository.DeleteAsync(CareMomentToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
