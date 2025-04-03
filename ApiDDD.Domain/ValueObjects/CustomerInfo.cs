using System;

namespace SalesAPI.Domain.ValueObjects
{
    public class CustomerInfo
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        private CustomerInfo() { }

        public CustomerInfo(Guid id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}