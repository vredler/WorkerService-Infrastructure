# WorkerService Infrastructure
Abstract infrastructure for setting up a worker service.

Currently only includes one way of setting up a worker service, the ScheduledWorker creates a schedule and runs the provided task on that schedule.

#### Current problems
- Only one task is run, even if multiple ScheduleWorkers are created.

#### Dependencies
- Microsoft.Extensions.Hosting.Abstractions 3.0.0
- NETStandard.Library 2.0.3
- ncrontab 3.2.0
- Serilog 2.8.0
