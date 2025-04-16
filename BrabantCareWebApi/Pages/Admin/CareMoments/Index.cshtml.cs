using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class IndexModel : PageModel
    {
        private readonly CareMomentRepository _careMomentRepository;

        public IndexModel(CareMomentRepository repository)
        {
            _careMomentRepository = repository;
        }

        public IEnumerable<CareMoment> CareMoments { get; set; }

        public async Task OnGetAsync()
        {
            CareMoments = await _careMomentRepository.ReadAsync();
        }
    }
}
