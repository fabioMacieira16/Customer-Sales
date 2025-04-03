using SalesAPI.Domain.Entities;
using System;

namespace SalesAPI.Domain.Events
{
    public class SaleCancelledEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime CancellationDate { get; }
        public decimal CancelledAmount { get; }
        public int ItemsCount { get; }

        public SaleCancelledEvent(Sale sale)
        {
            SaleId = sale.Id;
            SaleNumber = sale.SaleNumber;
            CancellationDate = DateTime.UtcNow;
            CancelledAmount = sale.TotalAmount;
            ItemsCount = sale.Items.Count;
        }
    }
}

