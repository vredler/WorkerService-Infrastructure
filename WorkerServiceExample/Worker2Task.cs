using System;
using System.Threading.Tasks;
using Serilog;
using WorkerService.Scheduled.Infrastructure;

namespace WorkerService1
{
    public class Worker2Task : IServiceWorkerTask
    {
        private readonly ILogger _logger;

        public Worker2Task(ILogger logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            _logger.Information("Worker 2 ran at: {time}", DateTimeOffset.Now);

            await Task.Delay(1);
        }
    }
}