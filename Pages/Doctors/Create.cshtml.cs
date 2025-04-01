using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;

namespace BrabantCareWebApi.Pages.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public Doctor newDoctor { get; set; }

        public CreateModel(DoctorRepository doctorRepository, ILogger<CreateModel> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
        }

        public void OnGet()
        {
            // Handle GET request (show form, etc.)
            _logger.LogInformation("OnGet triggered for doctor creation");
            newDoctor = new Doctor();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync triggered for doctor creation");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log errors
                }
                return Page(); // Return the form with validation errors
            }
            try
            {
                newDoctor.ID = Guid.NewGuid();
                await _doctorRepository.InsertAsync(newDoctor);
                _logger.LogInformation($"updatedDoctor {newDoctor.Name} successfully created.");

                return RedirectToPage("Index");  // Redirect after successful creation
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting the doctor.");
                return Page();  // Return to the page if there's an error
            }
        }
        //public void OnPost(updatedDoctor doctor)
        //{
        //    _logger.LogInformation("post called");
        //}

    }
}
