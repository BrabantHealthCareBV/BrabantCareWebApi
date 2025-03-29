using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Guardians
{
    public class DeleteModel : PageModel
    {
        private readonly GuardianRepository _guardianRepository;
        [BindProperty]
        public Guardian GuardianToDelete { get; set; } = new();
        public DeleteModel(GuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            GuardianToDelete = await _guardianRepository.ReadAsync(id);
            if (GuardianToDelete == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _guardianRepository.DeleteAsync(GuardianToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
