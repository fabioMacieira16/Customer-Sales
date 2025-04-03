using SalesAPI.Domain.Exceptions;
using SalesAPI.Domain.ValueObjects;
using System.Collections.Generic;
using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesAPI.Domain.Entities
{
    public class Sale
    {
        [BsonId]
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public CustomerInfo Customer { get; private set; }
        public BranchInfo Branch { get; private set; }
        public List<SaleItem> Items { get; set; }
        public bool IsCancelled { get; private set; }
        public decimal TotalAmount { get; private set; }

        private Sale() { }

        public Sale(string saleNumber, CustomerInfo customer, BranchInfo branch)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = DateTime.UtcNow;
            Customer = customer;
            Branch = branch;
            Items = new List<SaleItem>();
            IsCancelled = false;
            TotalAmount = 0;
        }

        public void AddItem(ProductInfo product, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new DomainException("Maximum quantity per product is 20 units");

            var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > 20)
                    throw new DomainException("Maximum quantity per product is 20 units");
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                Items.Add(new SaleItem(product, quantity, unitPrice));
            }

            CalculateTotalAmount();
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new DomainException("Sale is already cancelled");

            IsCancelled = true;
        }

        public void CancelItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null)
                throw new DomainException("Product not found in sale");

            Items.Remove(item);
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(item => item.TotalAmount);
        }
    }
}
