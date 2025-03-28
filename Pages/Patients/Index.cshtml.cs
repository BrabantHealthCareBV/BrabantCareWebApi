using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly GuardianRepository _guardianRepository;
        private readonly TreatmentPlanRepository _treatmentPlanRepository;
        private readonly DoctorRepository _doctorRepository;

        public IndexModel(PatientRepository patientRepository,
            GuardianRepository guardianRepository,
            TreatmentPlanRepository treatmentPlanRepository,
            DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
            _treatmentPlanRepository = treatmentPlanRepository;
            _doctorRepository = doctorRepository;
        }

        public List<Patient> Patients { get; set; }
        public List<Guardian> Guardians { get; set; }
        public List<TreatmentPlan> TreatmentPlans { get; set; }
        public List<Doctor> Doctors { get; set; }

        public async Task OnGetAsync()
        {
            Patients = (List<Patient>)await _patientRepository.ReadAsync();
            Guardians = (List<Guardian>)await _guardianRepository.ReadAsync();
            TreatmentPlans = (List<TreatmentPlan>)await _treatmentPlanRepository.ReadAsync();
            Doctors = (List<Doctor>)await _doctorRepository.ReadAsync();
        }
    }
}
