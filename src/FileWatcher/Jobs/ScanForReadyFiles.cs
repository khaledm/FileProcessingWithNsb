namespace FileWatcher.Jobs
{
    using System.Threading.Tasks;
    using Quartz;
    using NServiceBus;

    public class ScanForReadyFiles : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var endpointInstance = context.EndpointInstance();

            // SCAN the file Ready location
            // loop through the unique file names and send them to In Progress file location
            // send commands to start processing of the files

            await endpointInstance.Send("DEstination", new object())
                .ConfigureAwait(false);
        }
    }
}