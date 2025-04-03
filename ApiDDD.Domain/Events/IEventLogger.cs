using System;
using System.Threading.Tasks;

namespace SalesAPI.Domain.Events;

public interface IEventLogger
{
    Task LogEventAsync<TEvent>(TEvent @event) where TEvent : class;
    Task LogErrorAsync(string eventType, string message, Exception exception);
}
