using System.Collections.Generic;
using System;

namespace SalesAPI.Application.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public CustomerInfoDto Customer { get; set; }
        public BranchInfoDto Branch { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CustomerInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class BranchInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public ProductInfoDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ProductInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}