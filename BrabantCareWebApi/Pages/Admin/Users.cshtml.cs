using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrabantCareWebApi.Models;

public class UsersModel : PageModel
{
    private readonly UserRepository _userRepository;

    public UsersModel(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Users = await _userRepository.GetAllUsersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string userId)
    {
        await _userRepository.DeleteUserAsync(userId);
        return RedirectToPage();
    }
}
