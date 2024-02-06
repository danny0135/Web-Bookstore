using Microsoft.AspNetCore.Mvc;

namespace FinalProject.ViewModel
{
    public class CheckoutFormData
    {
        // Customer Billing information

        public string name { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public string city { get; set; } = string.Empty;

        public string province { get; set; } = string.Empty;

        public string postalCode { get; set; } = string.Empty;

        public string country { get; set; } = string.Empty;

        // Customer Payment information

        public long ccNumber { get; set; } 

        public string ccExpiryDate { get; set; } = string.Empty;

        public int cvv { get; set; } 

    }
}
