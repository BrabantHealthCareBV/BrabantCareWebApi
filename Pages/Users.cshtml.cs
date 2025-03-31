using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class UsersModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public List<IdentityUser> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Users = new List<IdentityUser>(await _userManager.Users.ToListAsync());
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToPage();
    }
}
