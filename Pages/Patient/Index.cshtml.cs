using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly PatientRepository _patientRepository;
        public IEnumerable<Patient> Patients { get; set; }

        public IndexModel(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task OnGetAsync()
        {
            Patients = await _patientRepository.ReadAsync();
        }
    }
}
