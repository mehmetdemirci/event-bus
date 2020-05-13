using Abstraction.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class SingleGroup2EventHandler : IEventHandler<SingleIntegrationEvent>
    {
        private readonly ILogger<SingleGroup2EventHandler> logger;

        public SingleGroup2EventHandler(ILogger<SingleGroup2EventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(SingleIntegrationEvent integrationEvent)
        {
            this.logger.LogInformation($"{nameof(SingleGroup2EventHandler)} handled {nameof(integrationEvent)}-{integrationEvent.EventId}");
            return Task.FromResult(true);
        }
    }
}