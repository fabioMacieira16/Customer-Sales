using FluentAssertions;
using Moq;
using SalesAPI.Application.DTOs;
using SalesAPI.Application.Services;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.Events;
using SalesAPI.Domain.Repositories;
using SalesAPI.Domain.ValueObjects;

namespace ApiDDD.Tests.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly Mock<IEventLogger> _eventLoggerMock;
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _eventLoggerMock = new Mock<IEventLogger>();
            _service = new SaleService(_repositoryMock.Object, _eventLoggerMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenSaleExists_ShouldReturnSale()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
            var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
            var expectedSale = new Sale("SALE001", customer, branch);

            _repositoryMock.Setup(x => x.GetByIdAsync(saleId))
                .ReturnsAsync(expectedSale);

            // Act
            var result = await _service.GetByIdAsync(saleId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedSale);
        }

        [Fact]
        public async Task GetByIdAsync_WhenSaleDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _repositoryMock.Setup(x => x.GetByIdAsync(saleId))
                .ReturnsAsync((Sale)null);

            // Act
            var result = await _service.GetByIdAsync(saleId);

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

            _repositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedSales);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedSales);
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldCreateSale()
        {
            // Arrange
            var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
            var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
            var createSaleDto = new CreateSaleDto
            {
                SaleNumber = "SALE001",
                Customer = new CustomerInfoDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone
                },
                Branch = new BranchInfoDto
                {
                    Id = branch.Id,
                    Name = branch.Name,
                    Address = branch.Address,
                    City = branch.City,
                    State = branch.State
                }
            };

            var expectedSale = new Sale("SALE001", customer, branch);
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Sale>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateSaleAsync(createSaleDto);

            // Assert
            result.Should().NotBeNull();
            result.SaleNumber.Should().Be(createSaleDto.SaleNumber);
            result.Customer.Id.Should().Be(createSaleDto.Customer.Id);
            result.Branch.Id.Should().Be(createSaleDto.Branch.Id);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ShouldReturnCustomerSales()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerInfo(customerId, "Test Customer", "test@email.com", "1234567890");
            var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
            var expectedSales = new List<Sale>
            {
                new Sale("SALE001", customer, branch),
                new Sale("SALE002", customer, branch)
            };

            _repositoryMock.Setup(x => x.GetByCustomerIdAsync(customerId))
                .ReturnsAsync(expectedSales);

            // Act
            var result = await _service.GetByCustomerIdAsync(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedSales);
        }

        [Fact]
        public async Task GetByBranchIdAsync_ShouldReturnBranchSales()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
            var branch = new BranchInfo(branchId, "Test Branch", "Test Address", "Test City", "Test State");
            var expectedSales = new List<Sale>
            {
                new Sale("SALE001", customer, branch),
                new Sale("SALE002", customer, branch)
            };

            _repositoryMock.Setup(x => x.GetByBranchIdAsync(branchId))
                .ReturnsAsync(expectedSales);

            // Act
            var result = await _service.GetByBranchIdAsync(branchId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedSales);
        }
    }
}