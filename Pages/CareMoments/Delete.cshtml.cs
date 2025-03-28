using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class DeleteModel : PageModel
    {
        private readonly CareMomentRepository _repository;

        public DeleteModel(CareMomentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public CareMoment CareMomentToDelete { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var moment = await _repository.ReadAsync(id);
            if (moment == null) return NotFound();
            CareMomentToDelete = moment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repository.DeleteAsync(CareMomentToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
