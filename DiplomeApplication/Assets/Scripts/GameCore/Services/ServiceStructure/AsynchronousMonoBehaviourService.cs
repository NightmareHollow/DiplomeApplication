using System.Threading;
using System.Threading.Tasks;

namespace GameCore.Services.ServiceStructure
{
    public abstract class AsynchronousMonoBehaviourService : MonoBehaviourService
    {
        public abstract Task InitializeService(CancellationToken cancellationToken);
    }
}