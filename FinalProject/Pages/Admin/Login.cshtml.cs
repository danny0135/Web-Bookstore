using FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinalProject.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly FinalProjectContext _context;

        [BindProperty]
        public String UserName { get; set; } = string.Empty;

        [BindProperty]
        public String Password { get; set; } = string.Empty;

        // Constructor
        public LoginModel(FinalProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Replace the hardcoded values with your desired username and password
            string hardcodedUsername = "Danny";
            string hardcodedPassword = "11111";

            // Check if the provided username and password match the hardcoded values
            if (UserName == hardcodedUsername && Password == hardcodedPassword)
            {
                // Setup session
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, hardcodedUsername),
                    new Claim("FullName", "Danny"), 
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties());

                return RedirectToPage("/Products/Index");
            }
            else
            {
                ModelState.AddModelError("UserName", "Incorrect username or password.");
                return Page();
            }
        }
    }
}