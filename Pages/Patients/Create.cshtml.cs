using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly GuardianRepository _guardianRepository;
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly DoctorRepository _doctorRepository;

        public CreateModel(PatientRepository patientRepository,
            GuardianRepository guardianRepository,
            TreatmentPlanRepository treatmentPlanRepository,
            DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _doctorRepository = doctorRepository;
        }

        [BindProperty]
        public Patient newPatient { get; set; }

        public List<Guardian> Guardians { get; set; }
        public List<TreatmentPlan> TreatmentPlans { get; set; }
        public List<Doctor> Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Guardians = (List<Guardian>)await _guardianRepository.ReadAsync();
            TreatmentPlans = (List<TreatmentPlan>)await _treatmentPlanRepository.ReadAsync();
            Doctors = (List<Doctor>)await _doctorRepository.ReadAsync();

            // Add checks to ensure they are not null or empty
            if (Guardians == null || TreatmentPlans == null || Doctors == null)
            {
                // Log or throw an exception here
                return Page(); // Or some error page if needed
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            newPatient.ID = Guid.NewGuid();  // Generate a new ID for the newPatient
            await _patientRepository.InsertAsync(newPatient);

            return RedirectToPage("/Patients/Index");
        }
    }
}
