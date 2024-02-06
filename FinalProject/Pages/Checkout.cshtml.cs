using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace FinalProject.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public CheckoutModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CheckoutFormData CheckoutForm { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Making sure the Checkout Model is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cart = Request.Cookies["ShoppingCart"];

            var jsonData = new
            {
                CheckoutForm.name,
                CheckoutForm.address,
                CheckoutForm.city,
                CheckoutForm.province,
                CheckoutForm.postalCode,
                CheckoutForm.country,
                CheckoutForm.ccNumber,
                CheckoutForm.ccExpiryDate,
                CheckoutForm.cvv,
                Products = cart != null ? string.Join(",", cart.Split(',')) : ""
            };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://nscc-inet2005-purchase-api.azurewebsites.net/purchase", content);

                if (response.IsSuccessStatusCode)
                {
                    Response.Cookies.Delete("ShoppingCart");
                    string confirmationNumber = await response.Content.ReadAsStringAsync();
                    return RedirectToPage("/Confirmation", new { confirmationNumber = confirmationNumber, name = CheckoutForm.name });
                }
                else
                {
                    ErrorMessage = "Error placing the order. Please try again.";
                    return Page();
                }
            }
        }
    }
}
