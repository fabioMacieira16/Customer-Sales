using SalesAPI.Domain.Entities;
using System;

namespace SalesAPI.Domain.Events
{
    public class ItemCancelledEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public Guid ProductId { get; }
        public string ProductName { get; }
        public int CancelledQuantity { get; }
        public decimal CancelledAmount { get; }
        public DateTime CancellationDate { get; }

        public ItemCancelledEvent(Sale sale, SaleItem item)
        {
            SaleId = sale.Id;
            SaleNumber = sale.SaleNumber;
            ProductId = item.Product.Id;
            ProductName = item.Product.Name;
            CancelledQuantity = item.Quantity;
            CancelledAmount = item.TotalAmount;
            CancellationDate = DateTime.UtcNow;
        }
    }
}
