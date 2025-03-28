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
        public Guardian deleteGuardian { get; set; } = new();

        public DeleteModel(GuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            deleteGuardian = await _guardianRepository.ReadAsync(id) ?? new Guardian();
            if (deleteGuardian == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (deleteGuardian.ID == Guid.Empty) return NotFound();
            await _guardianRepository.DeleteAsync(deleteGuardian.ID);
            return RedirectToPage("Index");
        }
    }
}
