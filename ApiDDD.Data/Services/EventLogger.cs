using Microsoft.Extensions.Logging;
using SalesAPI.Domain.Events;
using System;
using System.Threading.Tasks;

namespace SalesAPI.Infrastructure.Services
{
    public class EventLogger : IEventLogger
    {
        private readonly ILogger<EventLogger> _logger;

        public EventLogger(ILogger<EventLogger> logger)
        {
            _logger = logger;
        }

        public Task LogErrorAsync(string eventType, string message, Exception exception)
        {
            _logger.LogError(exception, $"Error Event: {eventType} - Message: {message}");
            return Task.CompletedTask;
        }

        public Task LogEventAsync<TEvent>(TEvent @event) where TEvent : class
        {
            var eventType = @event.GetType().Name;
            var eventData = System.Text.Json.JsonSerializer.Serialize(@event);
            _logger.LogInformation("Domain Event: {EventType} - {EventData}", eventType, eventData);
            return Task.CompletedTask;
        }
    }
}