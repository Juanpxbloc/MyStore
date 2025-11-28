using System.ComponentModel.DataAnnotations;

namespace MyStore.Entities
{
    public class Category // Represents a product category in the e-commerce system before the db migration
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } // Navigation property to represent the one-to-many relationship with products
    }
}