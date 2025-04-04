using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        [BindProperty]
        public Patient PatientToDelete { get; set; } = new();
        public DeleteModel(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            PatientToDelete = await _patientRepository.ReadAsync(id);
            if (PatientToDelete == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _patientRepository.DeleteAsync(PatientToDelete.ID);
            return RedirectToPage("Index");
        }
    }
}
