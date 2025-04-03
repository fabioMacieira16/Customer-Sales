using System;

namespace SalesAPI.Domain.ValueObjects
{
    public class BranchInfo
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        private BranchInfo() { }

        public BranchInfo(Guid id, string name, string address, string city, string state)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
        }
    }
}
