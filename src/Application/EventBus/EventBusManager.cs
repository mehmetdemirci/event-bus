using Abstraction.Events;
using DotNetCore.CAP;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventBus
{
    internal sealed class EventBusManager : IEventCollector, IEventDispatcher
    {
        private readonly ConcurrentQueue<IntegrationEvent> events;
        private readonly ICapPublisher capPublisher;

        public EventBusManager(ICapPublisher capPublisher)
        {
            this.capPublisher = capPublisher;
            this.events = new ConcurrentQueue<IntegrationEvent>();
        }

        public void Enqueue<T>(T @event)
            where T : IntegrationEvent
        {
            this.events.Enqueue(@event);
        }

        public Task EnqueueAsync<T>(T @event)
            where T : IntegrationEvent
        {
            Enqueue(@event);
            return Task.CompletedTask;
        }

        public void Publish()
        {
            while (this.events.IsEmpty == false)
            {
                if (this.events.TryDequeue(out var evt))
                {
                    this.capPublisher.Publish(evt.Name, evt);
                }
            }
        }

        public async Task PublishAsync(CancellationToken cancellationToken = default)
        {
            var publishEventTasks = new List<Task>();

            while (this.events.IsEmpty == false)
            {
                if (this.events.TryDequeue(out var evt))
                {
                    publishEventTasks.Add(this.capPublisher.PublishAsync(evt.Name, evt, cancellationToken: cancellationToken));
                }
            }

            await Task.WhenAll(publishEventTasks);
        }
    }
}