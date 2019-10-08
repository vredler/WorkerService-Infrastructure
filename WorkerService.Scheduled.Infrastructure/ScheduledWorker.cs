using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NCrontab;
using Serilog;

namespace WorkerService.Scheduled.Infrastructure
{
    public class ScheduledWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceWorkerTask _worker;
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        public ScheduledWorker(IServiceWorkerTask worker, string schedule, ILogger logger)
        {
            _logger = logger;
            _worker = worker;
            _schedule = CrontabSchedule.Parse(schedule);
            _nextRun = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                var now = DateTime.UtcNow;

                if (now > _nextRun)
                {
                    await _worker.ExecuteAsync();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                }

                await Task.Delay(1, stoppingToken);
            }
        }
    }
}