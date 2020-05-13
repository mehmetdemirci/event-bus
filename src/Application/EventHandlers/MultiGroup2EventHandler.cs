using Abstraction.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class MultiGroup2EventHandler : IEventHandler<GroupIntegrationEvent>
    {
        private readonly ILogger<MultiGroup2EventHandler> logger;

        public MultiGroup2EventHandler(ILogger<MultiGroup2EventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(GroupIntegrationEvent integrationEvent)
        {
            this.logger.LogInformation($"{nameof(MultiGroup2EventHandler)} handled {nameof(integrationEvent)}-{integrationEvent.EventId}");
            return Task.CompletedTask;
        }
    }
}