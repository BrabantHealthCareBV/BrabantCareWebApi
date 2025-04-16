using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Admin/Login");
        }

        return RedirectToPage("/Admin/Dashboard"); // or wherever your actual admin homepage is
    }
}
