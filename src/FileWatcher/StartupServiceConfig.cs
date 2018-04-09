namespace FileWatcher
{
    using System.Threading.Tasks;
    using NServiceBus;
    using FileWatcher.Jobs;
    using Quartz;
    using Quartz.Impl;

    public class StartupServiceConfig : IWantToRunWhenEndpointStartsAndStops
    {
        public StartupServiceConfig()
        {

        }

        public async Task Start(IMessageSession session)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler()
                .ConfigureAwait(false);

            scheduler.SetMessageSession(session);
            scheduler.SetReadyFolderPath(@"c:\TEMP\Ready");
            scheduler.SetInProgressFolderPath(@"c:\TEMP\InProgress");

            await scheduler.Start()
                .ConfigureAwait(false);

            // define the job and tie it to the SendMessageJob class
            var job = JobBuilder.Create<ScanForReadyFiles>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 3 seconds
            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(
                    action: builder =>
                    {
                        builder
                            .WithIntervalInSeconds(3)
                            .RepeatForever();
                    })
                .Build();

            // Tell quartz to schedule the job using the trigger
            await scheduler.ScheduleJob(job, trigger)
                .ConfigureAwait(false);
        }

        public async Task Stop(IMessageSession session)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}