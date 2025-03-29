using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;

namespace BrabantCareWebApi.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly GuardianRepository _guardianRepository;
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly DoctorRepository _doctorRepository;

        public EditModel(PatientRepository patientRepository,
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
        public Patient patient { get; set; }

        public List<Guardian> Guardians { get; set; }
        public List<TreatmentPlan> TreatmentPlans { get; set; }
        public List<Doctor> Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            // Fetch patient details and related dropdown lists
            patient = await _patientRepository.ReadAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            Guardians = (List<Guardian>)await _guardianRepository.ReadAsync();
            TreatmentPlans = (List<TreatmentPlan>)await _treatmentPlanRepository.ReadAsync();
            Doctors = (List<Doctor>)await _doctorRepository.ReadAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            Console.WriteLine($"Updating Patient: {patient.ID} - {patient.FirstName} {patient.LastName}, Birthdate: {patient.Birthdate}");

            await _patientRepository.UpdateAsync(patient);
            return RedirectToPage("/Patients/Index");
        }
    }
}
