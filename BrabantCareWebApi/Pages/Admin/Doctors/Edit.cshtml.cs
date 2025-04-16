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
        public Doctor updatedDoctor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            updatedDoctor = await _doctorRepository.ReadAsync(id);
            if (updatedDoctor == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            await _doctorRepository.UpdateAsync(updatedDoctor);
            return RedirectToPage("/Doctors/Index");
        }
    }
}
