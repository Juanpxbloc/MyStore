using System.ComponentModel.DataAnnotations;
using MyStore.Entities;
using MyStore.Entitiess;

namespace MyStore.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }

        public User? User { get; set; } // Navigation property for related User

        public ICollection<OrderItem> OrderItems { get; set; } // Navigation property for related OrderItems
    }
}