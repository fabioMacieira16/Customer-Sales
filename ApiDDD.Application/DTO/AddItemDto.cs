namespace SalesAPI.Application.DTOs
{
    public class AddItemDto
    {
        public ProductInfoDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
