using System;

namespace SalesAPI.Domain.ValueObjects
{
    public class ProductInfo
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Brand { get; private set; }

        private ProductInfo() { }

        public ProductInfo(Guid id, string name, string description, string category, string brand)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Brand = brand;
        }
    }
}
