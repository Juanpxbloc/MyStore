using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace MyStore.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public String? ImageName { get; set; } = null;
        public Category Category { get; set; } //Reference to Category entity for foreign key relationship
    }
}