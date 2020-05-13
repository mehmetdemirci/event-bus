using Abstraction.Events;
using Application.EventHandlers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class GroupCommandHandler : AsyncRequestHandler<GroupCommand>
    {
        private readonly IEventCollector eventCollector;
        private readonly IEventDispatcher eventDispatcher;

        public GroupCommandHandler(IEventCollector eventCollector,
            IEventDispatcher eventDispatcher)
        {
            this.eventCollector = eventCollector;
            this.eventDispatcher = eventDispatcher;
        }

        protected override async Task Handle(GroupCommand request, CancellationToken cancellationToken)
        {
            await eventCollector.EnqueueAsync(new GroupIntegrationEvent());

            await eventDispatcher.PublishAsync();
        }
    }
}