using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Repositories;
using BrabantCareWebApi.Models;

namespace BrabantCareWebApi.Pages.CareMoments
{
    public class CreateModel : PageModel
    {
        private readonly CareMomentRepository _careMomentRepository;

        public CreateModel(CareMomentRepository repository)
        {
            _careMomentRepository = repository;
        }

        [BindProperty]
        public CareMoment NewCareMoment { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            NewCareMoment.ID = Guid.NewGuid(); 
            await _careMomentRepository.InsertAsync(NewCareMoment);
            return RedirectToPage("Index");
        }
    }
}
