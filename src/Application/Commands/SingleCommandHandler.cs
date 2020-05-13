using Abstraction.Events;
using Application.EventHandlers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class SingleCommandHandler : AsyncRequestHandler<SingleCommand>
    {
        private readonly IEventCollector eventCollector;
        private readonly IEventDispatcher eventDispatcher;

        public SingleCommandHandler(IEventCollector eventCollector,
            IEventDispatcher eventDispatcher)
        {
            this.eventCollector = eventCollector;
            this.eventDispatcher = eventDispatcher;
        }

        protected async override Task Handle(SingleCommand request, CancellationToken cancellationToken)
        {
            await eventCollector.EnqueueAsync(new SingleIntegrationEvent());

            await eventDispatcher.PublishAsync();
        }
    }
}