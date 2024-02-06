using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Pages
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data.FinalProjectContext _context;

        public CustomerDetailsModel(Data.FinalProjectContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public int Quantity { get; set; }

        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int ProductId, string Name)
        {
            if (ProductId == 0 || string.IsNullOrEmpty(Name))
            {
                return Page();
            }

            // Add product to shopping cart (cookie)
            string cart = Request.Cookies["ShoppingCart"];


            if (string.IsNullOrEmpty(cart))
            {
                // cart is empty, initialize it
                Response.Cookies.Append("ShoppingCart", ProductId.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)

                });
            }

            else
            {
                // cart is not empty, append the new product ID
                var cartItems = cart.Split(',');
                cart = string.Join(",", cartItems.Append(ProductId.ToString()));

                // update the cart cookie
                Response.Cookies.Append("ShoppingCart", cart, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                });
            }

            // Redirect to the home page
            return RedirectToPage("/Index");
        }
    }
}