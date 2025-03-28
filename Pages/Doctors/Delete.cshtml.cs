using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using ProjectMap.WebApi.Repositories;

namespace BrabantCareWebApi.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorRepository _doctorRepository;

        public DeleteModel(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [BindProperty]
        public Guid DoctorId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _doctorRepository.DeleteAsync(DoctorId);
            return RedirectToPage("/Doctors/Index");
        }

        public async Task OnGetAsync(Guid id)
        {
            DoctorId = id;
        }
    }
}
