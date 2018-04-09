namespace FileRetriver
{
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.Serilog;
    using Serilog;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.EnableInstallers();

            LogManager.Use<SerilogFactory>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }
    }
}
