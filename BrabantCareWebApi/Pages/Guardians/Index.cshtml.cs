using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrabantCareWebApi.Pages.Guardians
{
    public class IndexModel : PageModel
    {
        private readonly GuardianRepository _guardianRepository;
        public List<Guardian> Guardians { get; set; } = new();

        public IndexModel(GuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public async Task OnGetAsync()
        {
            Guardians = (await _guardianRepository.ReadAsync()).ToList();
        }
    }
}
