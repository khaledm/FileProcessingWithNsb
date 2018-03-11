namespace FileWatcher
{
    using System.Threading.Tasks;
    using NServiceBus;

    public class StartupServiceConfig : IWantToRunWhenEndpointStartsAndStops
    {
        public StartupServiceConfig()
        {

        }

        public Task Start(IMessageSession session)
        {
            throw new System.NotImplementedException();
        }

        public Task Stop(IMessageSession session)
        {
            throw new System.NotImplementedException();
        }
    }
}