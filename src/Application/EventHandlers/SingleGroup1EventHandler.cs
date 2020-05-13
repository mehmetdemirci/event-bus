using Abstraction.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class SingleGroup1EventHandler : IEventHandler<SingleIntegrationEvent>
    {
        private readonly ILogger<SingleGroup1EventHandler> logger;

        public SingleGroup1EventHandler(ILogger<SingleGroup1EventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(SingleIntegrationEvent integrationEvent)
        {
            this.logger.LogInformation($"{nameof(SingleGroup1EventHandler)} handled {nameof(integrationEvent)}-{integrationEvent.EventId}");
            return Task.FromResult(true);
        }
    }
}