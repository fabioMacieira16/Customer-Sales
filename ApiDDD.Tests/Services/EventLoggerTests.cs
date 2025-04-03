using ApiDDD.Data.Events;
using Microsoft.Extensions.Logging;
using Moq;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.Events;
using SalesAPI.Domain.ValueObjects;

namespace ApiDDD.Tests.Services
{
    public class EventLoggerTests
    {
        private readonly Mock<ILogger<EventLogger>> _loggerMock;
        private readonly EventLogger _eventLogger;

        public EventLoggerTests()
        {
            _loggerMock = new Mock<ILogger<EventLogger>>();
            _eventLogger = new EventLogger(_loggerMock.Object);
        }

        [Fact]
        public async Task LogEventAsync_ShouldLogEvent()
        {
            // Arrange
            var customer = new CustomerInfo(Guid.NewGuid(), "Test Customer", "test@email.com", "1234567890");
            var branch = new BranchInfo(Guid.NewGuid(), "Test Branch", "Test Address", "Test City", "Test State");
            var sale = new Sale("SALE001", customer, branch);
            var @event = new SaleCreatedEvent(sale);

            // Act
            await _eventLogger.LogEventAsync(@event);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("SaleCreatedEvent")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task LogErrorAsync_ShouldLogError()
        {
            // Arrange
            var message = "Error processing event";
            var exception = new Exception("Test error");

            // Act
            await _eventLogger.LogErrorAsync(message, "SaleCreatedEvent", exception);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
                    exception,
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task LogEventAsync_WithNullEvent_ShouldNotThrowException()
        {
            // Arrange
            SaleCreatedEvent @event = null;

            // Act
            await _eventLogger.LogEventAsync(@event);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Event is null")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task LogErrorAsync_WithNullEvent_ShouldNotThrowException()
        {
            // Arrange
            var message = "Error processing null event";
            var exception = new Exception("Test error");

            // Act
            await _eventLogger.LogErrorAsync(message, null, exception);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
                    exception,
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}