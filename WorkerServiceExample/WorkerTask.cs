using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using WorkerService.Scheduled.Infrastructure;

namespace WorkerService1
{
    public class WorkerTask : IServiceWorkerTask
    {
        private readonly ILogger _logger;

        public WorkerTask(ILogger logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
                _logger.Information("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
                await Task.Delay(1);
        }
    }
}
