using Microsoft.Extensions.Logging;
using SalesAPI.Domain.Events;
using System;
using System.Threading.Tasks;

namespace ApiDDD.Data.Events;
public class EventLogger : IEventLogger
{
    private readonly ILogger<EventLogger> _logger;

    public EventLogger(ILogger<EventLogger> logger)
    {
        _logger = logger;
    }

    public async Task LogEventAsync<TEvent>(TEvent @event) where TEvent : class
    {
        _logger.LogInformation($"Event: {typeof(TEvent).Name}");
        _logger.LogInformation($"Data: {System.Text.Json.JsonSerializer.Serialize(@event)}");
        await Task.CompletedTask;
    }

    public async Task LogErrorAsync(string eventType, string message, Exception exception)
    {
        _logger.LogError(exception, $"Error Event: {eventType} - Message: {message}");
        await Task.CompletedTask;
    }
}
