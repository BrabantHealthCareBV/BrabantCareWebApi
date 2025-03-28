using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Doctors
{
    public class IndexModel : PageModel
    {
        private readonly DoctorRepository _doctorRepository;

        public IndexModel(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<Doctor> Doctors { get; set; }

        public async Task OnGetAsync()
        {
            Doctors = await _doctorRepository.ReadAsync();
        }
    }
}
