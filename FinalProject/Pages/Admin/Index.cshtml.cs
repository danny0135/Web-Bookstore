using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            return RedirectToPage("Admin/Login");
        }
    }
}
