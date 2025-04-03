using SalesAPI.Domain.Entities;
using System;

namespace SalesAPI.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime SaleDate { get; }
        public Guid CustomerId { get; }
        public string CustomerName { get; }
        public Guid BranchId { get; }
        public string BranchName { get; }

        public SaleCreatedEvent(Sale sale)
        {
            SaleId = sale.Id;
            SaleNumber = sale.SaleNumber;
            SaleDate = sale.SaleDate;
            CustomerId = sale.Customer.Id;
            CustomerName = sale.Customer.Name;
            BranchId = sale.Branch.Id;
            BranchName = sale.Branch.Name;
        }
    }
}