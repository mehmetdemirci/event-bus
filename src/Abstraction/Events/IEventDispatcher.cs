using System.Threading;
using System.Threading.Tasks;

namespace Abstraction.Events
{
    public interface IEventDispatcher
    {
        void Publish();

        Task PublishAsync(CancellationToken cancellationToken = default);
    }
}