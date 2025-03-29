using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.Doctors
{
    public class EditModel : PageModel
    {
        private readonly DoctorRepository _doctorRepository;

        public EditModel(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Doctor = await _doctorRepository.ReadAsync(id);
            if (Doctor == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _doctorRepository.UpdateAsync(Doctor);
            return RedirectToPage("/Doctors/Index");
        }
    }
}
