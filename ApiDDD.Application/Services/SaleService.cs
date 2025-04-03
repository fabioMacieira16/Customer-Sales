using SalesAPI.Application.DTOs;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.Events;
using SalesAPI.Domain.Repositories;
using SalesAPI.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SalesAPI.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventLogger _eventLogger;

    public SaleService(ISaleRepository saleRepository, IEventLogger eventLogger)
    {
        _saleRepository = saleRepository;
        _eventLogger = eventLogger;
    }

    public async Task<SaleDto> GetByIdAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        return MapToDto(sale);
    }

    public async Task<SaleDto> GetBySaleNumberAsync(string saleNumber)
    {
        var sale = await _saleRepository.GetBySaleNumberAsync(saleNumber);
        return MapToDto(sale);
    }

    public async Task<IEnumerable<SaleDto>> GetAllAsync()
    {
        var sales = await _saleRepository.GetAllAsync();
        return MapToDtos(sales);
    }

    public async Task<IEnumerable<SaleDto>> GetByCustomerIdAsync(Guid customerId)
    {
        var sales = await _saleRepository.GetByCustomerIdAsync(customerId);
        return MapToDtos(sales);
    }

    public async Task<IEnumerable<SaleDto>> GetByBranchIdAsync(Guid branchId)
    {
        var sales = await _saleRepository.GetByBranchIdAsync(branchId);
        return MapToDtos(sales);
    }

    public async Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto)
    {
        var customer = new CustomerInfo(
            createSaleDto.Customer.Id,
            createSaleDto.Customer.Name,
            createSaleDto.Customer.Email,
            createSaleDto.Customer.Phone
        );

        var branch = new BranchInfo(
            createSaleDto.Branch.Id,
            createSaleDto.Branch.Name,
            createSaleDto.Branch.Address,
            createSaleDto.Branch.City,
            createSaleDto.Branch.State
        );

        var sale = new Sale(createSaleDto.SaleNumber, customer, branch);
        await _saleRepository.AddAsync(sale);

        var saleCreatedEvent = new SaleCreatedEvent(sale);
        await _eventLogger.LogEventAsync(saleCreatedEvent);

        return MapToDto(sale);
    }

    public async Task<SaleDto> AddItemAsync(Guid saleId, AddItemDto addItemDto)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new ApplicationException("Sale not found");

        var product = new ProductInfo(
            addItemDto.Product.Id,
            addItemDto.Product.Name,
            addItemDto.Product.Description,
            addItemDto.Product.Category,
            addItemDto.Product.Brand
        );

        sale.AddItem(product, addItemDto.Quantity, addItemDto.UnitPrice);
        await _saleRepository.UpdateAsync(sale);

        var saleModifiedEvent = new SaleModifiedEvent(sale);
        await _eventLogger.LogEventAsync(saleModifiedEvent);

        return MapToDto(sale);
    }

    public async Task<SaleDto> CancelSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new ApplicationException("Sale not found");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale);

        var saleCancelledEvent = new SaleCancelledEvent(sale);
        await _eventLogger.LogEventAsync(saleCancelledEvent);

        return MapToDto(sale);
    }

    public async Task<SaleDto> CancelItemAsync(Guid saleId, Guid productId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new ApplicationException("Sale not found");

        var item = sale.Items.FirstOrDefault(i => i.Product.Id == productId);
        if (item == null)
            throw new ApplicationException("Product not found in sale");

        sale.CancelItem(productId);
        await _saleRepository.UpdateAsync(sale);

        var itemCancelledEvent = new ItemCancelledEvent(sale, item);
        await _eventLogger.LogEventAsync(itemCancelledEvent);

        return MapToDto(sale);
    }

    private static SaleDto MapToDto(Sale sale)
    {
        if (sale == null)
            return null;

        return new SaleDto
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber,
            SaleDate = sale.SaleDate,
            Customer = new CustomerInfoDto
            {
                Id = sale.Customer.Id,
                Name = sale.Customer.Name,
                Email = sale.Customer.Email,
                Phone = sale.Customer.Phone
            },
            Branch = new BranchInfoDto
            {
                Id = sale.Branch.Id,
                Name = sale.Branch.Name,
                Address = sale.Branch.Address,
                City = sale.Branch.City,
                State = sale.Branch.State
            },
            Items = sale.Items.Select(item => new SaleItemDto
            {
                Id = item.Id,
                Product = new ProductInfoDto
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    Description = item.Product.Description,
                    Category = item.Product.Category,
                    Brand = item.Product.Brand
                },
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                TotalAmount = item.TotalAmount
            }).ToList(),
            IsCancelled = sale.IsCancelled,
            TotalAmount = sale.TotalAmount
        };
    }

    private static IEnumerable<SaleDto> MapToDtos(IEnumerable<Sale> sales)
    {
        return sales.Select(MapToDto);
    }
}
