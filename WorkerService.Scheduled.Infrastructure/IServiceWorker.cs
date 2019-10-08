using System.Threading.Tasks;

namespace WorkerService.Scheduled.Infrastructure
{
    public interface IServiceWorkerTask
    {
        Task ExecuteAsync();
    }
}