using FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public List<Product> CartItems { get; private set; }

        private readonly FinalProject.Data.FinalProjectContext _context;

        public ShoppingCartModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public List<Product> CartProducts { get; set; }
        public List<int> Quantities { get; set; }
        public decimal TotalPriceBeforeTax { get; set; }
        public decimal TaxAmount { get; set; }
        public double TaxRate = 0.15;
        public double DeliveryCharge = 10.00;
        public decimal OrderTotal { get; set; }

        public IActionResult OnGet()
        {
            LoadCart();
            return Page();
        }

        private void LoadCart()
        {
            var cartCookie = Request.Cookies["ShoppingCart"];

            if (string.IsNullOrEmpty(cartCookie))
            {
                CartProducts = new List<Product>();
                Quantities = new List<int>();
            }
            else
            {
                var productIds = cartCookie.Split(',').Select(int.Parse).ToList();

                CartProducts = _context.Product
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToList();

                // Initialize quantities list with default values
                Quantities = new List<int>(new int[CartProducts.Count]);

                // Update quantities based on product IDs
                for (var i = 0; i < CartProducts.Count; i++)
                {
                    var productId = CartProducts[i].ProductId;
                    Quantities[i] = productIds.Count(id => id == productId);
                }
            }

            TotalPriceBeforeTax = CartProducts.Select((p, i) => p.Price * Quantities[i]).Sum();

            TaxAmount = TotalPriceBeforeTax * (decimal)TaxRate;

            OrderTotal = TotalPriceBeforeTax + TaxAmount + (decimal)DeliveryCharge;
        }
    }

}