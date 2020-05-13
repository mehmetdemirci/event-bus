using System.Threading.Tasks;

namespace Abstraction.Events
{
    public interface IEventCollector
    {
        void Enqueue<T>(T integrationEvent)
            where T : IntegrationEvent;

        Task EnqueueAsync<T>(T integrationEvent)
            where T : IntegrationEvent;
    }
}