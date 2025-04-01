using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Guardians
{
    public class EditModel : PageModel
    {
        private readonly GuardianRepository _guardianRepository;
        [BindProperty]
        public Guardian updatedGuardian { get; set; }
        public EditModel(GuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            updatedGuardian = await _guardianRepository.ReadAsync(id) ?? new Guardian();
            if (updatedGuardian == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _guardianRepository.UpdateAsync(updatedGuardian);
            return RedirectToPage("Index");
        }
    }
}
