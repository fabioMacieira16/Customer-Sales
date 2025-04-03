namespace SalesAPI.Application.DTOs
{
    public class CreateSaleDto
    {
        public string SaleNumber { get; set; }
        public CustomerInfoDto Customer { get; set; }
        public BranchInfoDto Branch { get; set; }
    }
}
