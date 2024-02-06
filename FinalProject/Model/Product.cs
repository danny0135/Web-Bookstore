using System.ComponentModel;

namespace FinalProject.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        [DisplayName("Author(s)")]
        public string AuthorName { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        [DisplayName("Thumbnail")]
        public string Image { get; set; } = string.Empty;

    }
}
