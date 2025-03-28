using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly GuardianRepository _guardianRepository;
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly DoctorRepository _doctorRepository;

        [BindProperty]
        public Patient ExistingPatient { get; set; } = new();

        public List<SelectListItem> Guardians { get; set; } = new();
        public List<SelectListItem> TreatmentPlans { get; set; } = new();
        public List<SelectListItem> Doctors { get; set; } = new();

        public EditModel(PatientRepository patientRepository, GuardianRepository guardianRepository,
                         TreatmentPlanRepository treatmentPlanRepository, DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            ExistingPatient = await _patientRepository.ReadAsync(id);
            if (ExistingPatient == null) return NotFound();

            Guardians = (await _guardianRepository.ReadAsync())
                .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = $"{g.FirstName} {g.LastName}" })
                .ToList();

            TreatmentPlans = (await _treatmentPlanRepository.ReadAsync())
                .Select(tp => new SelectListItem { Value = tp.ID.ToString(), Text = tp.Name })
                .ToList();

            Doctors = (await _doctorRepository.ReadAsync())
                .Select(d => new SelectListItem { Value = d.ID.ToString(), Text = d.Name })
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _patientRepository.UpdateAsync(ExistingPatient);
            return RedirectToPage("Index");
        }
    }
}
