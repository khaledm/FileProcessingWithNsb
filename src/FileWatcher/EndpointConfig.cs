namespace FileWatcher
{
    using NServiceBus;
    using Serilog;
    using NServiceBus.Logging;
    using NServiceBus.Serilog;
    using FileProcessing.Messages.Commands;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(StartProcessingFile), "FileRetriver");

            endpointConfiguration.EnableInstallers();

            LogManager.Use<SerilogFactory>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }
    }
}
