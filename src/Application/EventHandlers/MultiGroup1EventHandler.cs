using Abstraction.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class MultiGroup1EventHandler : IEventHandler<GroupIntegrationEvent>
    {
        private readonly ILogger<MultiGroup1EventHandler> logger;

        public MultiGroup1EventHandler(ILogger<MultiGroup1EventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(GroupIntegrationEvent integrationEvent)
        {
            this.logger.LogInformation($"{nameof(MultiGroup1EventHandler)} handled {nameof(integrationEvent)}-{integrationEvent.EventId}");
            return Task.CompletedTask;
        }
    }
}