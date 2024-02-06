using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Pages.Products
{
    [Authorize]
    public class EditModel : PageModel
    {
        IWebHostEnvironment _env;
        private readonly FinalProject.Data.FinalProjectContext _context;

        [BindProperty]
        public IFormFile? ImageUpload { get; set; }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public EditModel(FinalProject.Data.FinalProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                // Generate a unique image name
                string newImage = Guid.NewGuid() + Path.GetExtension(ImageUpload.FileName);

                // Save the new image to the server
                string imagePath = Path.Combine(_env.WebRootPath, "photos", newImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                // Delete the old image (if it's not the default image)
                if (!string.IsNullOrEmpty(Product.Image) && Product.Image != "default.png")
                {
                    string oldImagePath = Path.Combine(_env.WebRootPath, "photos", Product.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Update the ImageName property with the new image name
                Product.Image = newImage;
            }

            // Update other properties
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
