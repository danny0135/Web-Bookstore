using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Pages.Products
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public DetailsModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }
            // Add product to shopping cart (cookie)
            string cart = Request.Cookies["ShoppingCart"];

            if (string.IsNullOrEmpty(cart))
            {
                // cart is empty, initialize it
                Response.Cookies.Append("ShoppingCart", Product.ProductId.ToString());
            }

            else
            {
                // add product to cart
                Response.Cookies.Append("ShoppingCart", cart + "," + Product.ProductId);
            }

            return Page();
        }
    }
}
