using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorRepository _doctorRepository;

        public DeleteModel(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [BindProperty]
        public Doctor DoctorToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var doctor = await _doctorRepository.ReadAsync(id);
            if (doctor == null) return NotFound();
            DoctorToDelete = doctor;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _doctorRepository.DeleteAsync(DoctorToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
