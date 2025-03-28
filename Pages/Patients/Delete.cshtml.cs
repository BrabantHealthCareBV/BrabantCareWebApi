using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BrabantCareWebApi.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly PatientRepository _patientRepository;

        [BindProperty]
        public Patient ExistingPatient { get; set; } = new();

        public DeleteModel(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ExistingPatient = await _patientRepository.ReadAsync(id);
            if (ExistingPatient == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var patient = await _patientRepository.ReadAsync(id);
            if (patient == null) return NotFound();

            await _patientRepository.DeleteAsync(id);
            return RedirectToPage("Index");
        }
    }
}
