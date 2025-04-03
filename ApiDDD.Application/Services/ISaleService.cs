using SalesAPI.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SalesAPI.Application.Services;
public interface ISaleService
{
    Task<SaleDto> GetByIdAsync(Guid id);
    Task<SaleDto> GetBySaleNumberAsync(string saleNumber);
    Task<IEnumerable<SaleDto>> GetAllAsync();
    Task<IEnumerable<SaleDto>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<SaleDto>> GetByBranchIdAsync(Guid branchId);
    Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto);
    Task<SaleDto> AddItemAsync(Guid saleId, AddItemDto addItemDto);
    Task<SaleDto> CancelSaleAsync(Guid saleId);
    Task<SaleDto> CancelItemAsync(Guid saleId, Guid productId);
}
