using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class IndexModel : PageModel
    {
        private readonly CareMomentRepository _repository;

        public IndexModel(CareMomentRepository repository)
        {
            _repository = repository;
        }

        public List<CareMoment> CareMoments { get; set; } = new();

        public async Task OnGetAsync()
        {
            CareMoments = (await _repository.ReadAsync()).ToList();
        }
    }
}
