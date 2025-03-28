using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Composition;

namespace BrabantCareWebApi.Pages
{
    public class ReportBugModel : PageModel
    {
        public void OnGet()
        {
        }
        public Report Report { get; set; }   //add this...
        public void OnPost(Report report)
        {
        }
    }

}
