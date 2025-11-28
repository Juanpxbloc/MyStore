using System.ComponentModel.DataAnnotations;
using MyStore.Entities;
namespace MyStore.Entitiess

{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Type { get; set; } // e.g., "Customer", "Admin
        
        public ICollection<Order> Orders { get; set; } // Navigation property for related orders
    }
}