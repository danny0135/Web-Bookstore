using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FinalProjectContext _context;

        public IList<Product> Products { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, FinalProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            if (_context != null)
            {
                Products = _context.Product.OrderByDescending(d => d).ToList();
            }
        }
    }
}