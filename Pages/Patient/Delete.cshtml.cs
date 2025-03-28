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
        public PatientViewModel ExistingPatient { get; set; } = new();

        public DeleteModel(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ExistingPatient = await _patientRepository.GetPatientWithDetailsByIdAsync(id);
            if (ExistingPatient == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _patientRepository.DeleteAsync(ExistingPatient.Id);
            return RedirectToPage("Index");
        }
    }
}
