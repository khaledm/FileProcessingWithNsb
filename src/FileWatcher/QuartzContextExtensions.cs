namespace FileWatcher
{
    using NServiceBus;
    using Quartz;

    public static class QuartzContextExtensions
    {
        public static IMessageSession MessageSession(this IJobExecutionContext context)
        {
            return (IMessageSession) context.Scheduler.Context["MessageSession"];
        }

        public static void SetMessageSession(this IScheduler scheduler, IMessageSession instance)
        {
            scheduler.Context["MessageSession"] = instance;
        }

        public static string ReadyFolderPath(this IJobExecutionContext context)
        {
            return context.Scheduler.Context["ReadyFolderPath"] as string;
        }

        public static string InProgressFolderPath(this IJobExecutionContext context)
        {
            return context.Scheduler.Context["InProgressFolderPath"] as string;
        }

        public static void SetReadyFolderPath(this IScheduler scheduler, string readyFolderPath)
        {
            scheduler.Context["ReadyFolderPath"] = readyFolderPath;
        }

        public static void SetInProgressFolderPath(this IScheduler scheduler, string inprogressFolderPath)
        {
            scheduler.Context["InProgressFolderPath"] = inprogressFolderPath;
        }
    }
}