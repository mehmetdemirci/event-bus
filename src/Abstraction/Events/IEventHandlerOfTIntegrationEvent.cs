using System.Threading.Tasks;

namespace Abstraction.Events
{
    public interface IEventHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent);
    }
}