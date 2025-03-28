using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly GuardianRepository _guardianRepository;
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly DoctorRepository _doctorRepository;

        [BindProperty]
        public Patient newPatient { get; set; } = new();

        public List<SelectListItem> Guardians { get; set; } = new();
        public List<SelectListItem> TreatmentPlans { get; set; } = new();
        public List<SelectListItem> Doctors { get; set; } = new();

        public CreateModel(PatientRepository patientRepository, GuardianRepository guardianRepository,
                           TreatmentPlanRepository treatmentPlanRepository, DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task OnGetAsync()
        {
            Guardians = (await _guardianRepository.ReadAsync())
                .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = $"{g.FirstName} {g.LastName}" })
                .ToList();

            TreatmentPlans = (await _treatmentPlanRepository.ReadAsync())
                .Select(tp => new SelectListItem { Value = tp.ID.ToString(), Text = tp.Name })
                .ToList();

            Doctors = (await _doctorRepository.ReadAsync())
                .Select(d => new SelectListItem { Value = d.ID.ToString(), Text = d.Name })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            newPatient.ID = Guid.NewGuid();
            await _patientRepository.InsertAsync(newPatient);
            return RedirectToPage("Index");
        }
    }
}
