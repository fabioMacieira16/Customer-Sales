using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDDD.Data.Repositories;
public class SaleRepository : ISaleRepository
{
    private readonly IMongoCollection<Sale> _sales;
    private readonly ILogger<SaleRepository> _logger;

    public SaleRepository(IConfiguration configuration, ILogger<SaleRepository> logger)
    {
        _logger = logger;
        var connectionString = configuration.GetConnectionString("MongoDB");
        _logger.LogInformation($"Connecting to MongoDB at: {connectionString}");

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("SalesDB");
        _sales = database.GetCollection<Sale>("Sales");

        _logger.LogInformation("Successfully connected to MongoDB");
    }

    public async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _sales.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Sale> GetBySaleNumberAsync(string saleNumber)
    {
        return await _sales.Find(s => s.SaleNumber == saleNumber).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _sales.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _sales.Find(s => s.Customer.Id == customerId).ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetByBranchIdAsync(Guid branchId)
    {
        return await _sales.Find(s => s.Branch.Id == branchId).ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        _logger.LogInformation($"Adding new sale with ID: {sale.Id}");
        await _sales.InsertOneAsync(sale);
        _logger.LogInformation($"Successfully added sale with ID: {sale.Id}");
    }

    public async Task UpdateAsync(Sale sale)
    {
        _logger.LogInformation($"Updating sale with ID: {sale.Id}");
        await _sales.ReplaceOneAsync(s => s.Id == sale.Id, sale);
        _logger.LogInformation($"Successfully updated sale with ID: {sale.Id}");
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation($"Deleting sale with ID: {id}");
        await _sales.DeleteOneAsync(s => s.Id == id);
        _logger.LogInformation($"Successfully deleted sale with ID: {id}");
    }
}