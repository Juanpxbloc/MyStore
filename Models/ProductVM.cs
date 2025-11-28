using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyStore.Models
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public CategoryVM Category { get; set; }

        public List<SelectListItem> Categories { get; set; } //Dropdown list of categories
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public int Stock { get; set; }
        public String? ImageName { get; set; } = null;

        public IFormFile? ImageFile { get; set; } // To obtain an image from the view
    }
}