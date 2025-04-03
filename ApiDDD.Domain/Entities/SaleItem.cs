using SalesAPI.Domain.ValueObjects;
using SalesAPI.Domain.Exceptions;
using System;

namespace SalesAPI.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public ProductInfo Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public Guid SaleId { get; private set; }
        public Sale Sales { get; private set; }

        private SaleItem() { }

        public SaleItem(ProductInfo product, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CalculateDiscount();
            CalculateTotalAmount();
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity > 20)
                throw new DomainException("Maximum quantity per product is 20 units");

            Quantity = newQuantity;
            CalculateDiscount();
            CalculateTotalAmount();
        }

        private void CalculateDiscount()
        {
            if (Quantity < 4)
            {
                Discount = 0;
            }
            else if (Quantity >= 4 && Quantity < 10)
            {
                Discount = 0.10m; // 10% discount
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = 0.20m; // 20% discount
            }
        }

        private void CalculateTotalAmount()
        {
            var subtotal = Quantity * UnitPrice;
            var discountAmount = subtotal * Discount;
            TotalAmount = subtotal - discountAmount;
        }
    }
}

