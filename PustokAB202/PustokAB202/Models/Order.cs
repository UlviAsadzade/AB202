using PustokAB202.Utilities.Enums;

namespace PustokAB202.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public decimal TotalAmount { get; set; }

        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
}
