using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using WorkerService.Scheduled.Infrastructure;

namespace WorkerService1
{
    public class Program
    {
        private const string WORKER_CRONTIMER = "*/1 * * * *";
        private const string WORKER_2_CRONTIMER = "*/2 * * * *";

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(
                    LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .CreateLogger();

            var testLogger = Log.Logger;

            testLogger.Information("Testing logger");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var worker1 = new ScheduledWorker(
                        new WorkerTask(Log.Logger),
                        WORKER_CRONTIMER,
                        Log.Logger
                    );

                    var worker2 = new ScheduledWorker(
                        new Worker2Task(Log.Logger),
                        WORKER_2_CRONTIMER,
                        Log.Logger
                    );

        services.AddLogging(builder => { builder.AddSerilog(); });
                    services.AddHostedService(f =>
                        worker1);
                    services.AddHostedService(f =>
                        worker2);
                });
    }
}
