using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Data;
using FinalProject.Model;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;
        IWebHostEnvironment _env;

        [BindProperty]
        public Product product { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageUpload { get; set; }
        public CreateModel(FinalProject.Data.FinalProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Make a unique image name
            string image = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-") + ImageUpload.FileName;

            Product.Image = image;

            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            //
            // Upload the Image to the www/photo folder
            //

            string file = _env.ContentRootPath + "\\wwwroot\\photos\\" + image;

            using (FileStream fileStream = new FileStream(file, FileMode.Create))
            {
                ImageUpload.CopyTo(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}
