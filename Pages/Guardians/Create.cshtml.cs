using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Guardians
{
    public class CreateModel : PageModel
    {
        private readonly GuardianRepository _guardianRepository;

        [BindProperty]
        public Guardian newGuardian { get; set; } = new();

        public CreateModel(GuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _guardianRepository.InsertAsync(newGuardian);
            return RedirectToPage("Index");
        }
    }
}
