using SalesAPI.Domain.Entities;
using System;

namespace SalesAPI.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime ModifiedDate { get; }
        public decimal NewTotalAmount { get; }
        public int ItemsCount { get; }

        public SaleModifiedEvent(Sale sale)
        {
            SaleId = sale.Id;
            SaleNumber = sale.SaleNumber;
            ModifiedDate = DateTime.UtcNow;
            NewTotalAmount = sale.TotalAmount;
            ItemsCount = sale.Items.Count;
        }
    }
}
