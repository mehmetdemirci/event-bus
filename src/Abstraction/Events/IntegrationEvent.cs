using System;

namespace Abstraction.Events
{
    public class IntegrationEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();

        public string Name => GetType().FullName;

        public DateTime OccurredOnUtc { get; set; } = DateTime.UtcNow;
    }
}