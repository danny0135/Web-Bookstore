using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public ConfirmationModel (FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public string ConfirmationNumber { get; set; }
        public string Name { get; set; }

        public void OnGet(string confirmationNumber, string name)
        {
            ConfirmationNumber = confirmationNumber;
            Name = name;

            // Remove double quotes if present in ConfirmationNumber
            ConfirmationNumber = ConfirmationNumber?.Trim('"');
        }
    }
}
