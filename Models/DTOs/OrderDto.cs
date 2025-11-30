namespace OrderMgmtAPI.Models.DTOs
{
    public class OrderDto
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
