using ApiDDD.Data.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.ValueObjects;

namespace ApiDDD.Tests.Repositories;

public class SaleRepositoryTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<SaleRepository>> _loggerMock;
    private readonly Mock<IMongoCollection<Sale>> _collectionMock;
    private readonly Mock<IMongoDatabase> _databaseMock;
    private readonly Mock<IMongoClient> _clientMock;
    private readonly SaleRepository _repository;

    public SaleRepositoryTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<SaleRepository>>();
        _collectionMock = new Mock<IMongoCollection<Sale>>();
        _databaseMock = new Mock<IMongoDatabase>();
        _clientMock = new Mock<IMongoClient>();

        _configurationMock.Setup(x => x.GetConnectionString("MongoDB"))
            .Returns("mongodb://localhost:27017");

        _databaseMock.Setup(x => x.GetCollection<Sale>("Sales", null))
            .Returns(_collectionMock.Object);

        _clientMock.Setup(x => x.GetDatabase("SalesDB", null))
            .Returns(_databaseMock.Object);

        _repository = new SaleRepository(_configurationMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenSaleExists_ShouldReturnSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
        var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
        var sale = new Sale("SALE001", customer, branch);

        var mockCursor = new Mock<IAsyncCursor<Sale>>();
        mockCursor.Setup(x => x.Current).Returns(new List<Sale> { sale });
        mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
            .Returns(true)
            .Returns(false);

        _collectionMock.Setup(x => x.FindAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            It.IsAny<FindOptions<Sale>>(),
            It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetByIdAsync(saleId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sale);
    }

    [Fact]
    public async Task GetByIdAsync_WhenSaleDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var mockCursor = new Mock<IAsyncCursor<Sale>>();
        mockCursor.Setup(x => x.Current).Returns(new List<Sale>());
        mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
            .Returns(true)
            .Returns(false);

        _collectionMock.Setup(x => x.FindAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            It.IsAny<FindOptions<Sale>>(),
            It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetByIdAsync(saleId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSales()
    {
        // Arrange
        var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
        var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
        var expectedSales = new List<Sale>
        {
            new Sale("SALE001", customer, branch),
            new Sale("SALE002", customer, branch)
        };

        var mockCursor = new Mock<IAsyncCursor<Sale>>();
        mockCursor.Setup(x => x.Current).Returns(expectedSales);
        mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
            .Returns(true)
            .Returns(false);

        _collectionMock.Setup(x => x.FindAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            It.IsAny<FindOptions<Sale>>(),
            It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expectedSales);
    }

    [Fact]
    public async Task AddAsync_ShouldInsertSale()
    {
        // Arrange
        var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
        var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
        var sale = new Sale("SALE001", customer, branch);

        _collectionMock.Setup(x => x.InsertOneAsync(
            sale,
            null,
            It.IsAny<System.Threading.CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _repository.AddAsync(sale);

        // Assert
        _collectionMock.Verify(x => x.InsertOneAsync(
            sale,
            null,
            It.IsAny<System.Threading.CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateSale()
    {
        // Arrange
        var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
        var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
        var sale = new Sale("SALE001", customer, branch);

        var replaceResult = new ReplaceOneResult.Acknowledged(1, 1, null);
        _collectionMock.Setup(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            sale,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(replaceResult);

        // Act
        await _repository.UpdateAsync(sale);

        // Assert
        _collectionMock.Verify(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            sale,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<System.Threading.CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var deleteResult = new DeleteResult.Acknowledged(1);
        _collectionMock.Setup(x => x.DeleteOneAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(deleteResult);

        // Act
        await _repository.DeleteAsync(saleId);

        // Assert
        _collectionMock.Verify(x => x.DeleteOneAsync(
            It.IsAny<FilterDefinition<Sale>>(),
            It.IsAny<System.Threading.CancellationToken>()),
            Times.Once);
    }
}