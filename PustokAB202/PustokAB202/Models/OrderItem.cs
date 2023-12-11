namespace PustokAB202.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Discount { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string BookName { get; set; }

        public string Image { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public decimal Subtotal { get; set; }


    }
}
