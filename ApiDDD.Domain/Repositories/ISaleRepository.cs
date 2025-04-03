using SalesAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SalesAPI.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(Guid id);
    Task<Sale> GetBySaleNumberAsync(string saleNumber);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid branchId);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
}
