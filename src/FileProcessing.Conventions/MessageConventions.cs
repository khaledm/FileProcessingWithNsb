namespace FileProcessing.Conventions
{
    using System;
    using NServiceBus;

    public class MessageConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            var conventionsBuilder = configuration.Conventions();
            conventionsBuilder.DefiningCommandsAs(t => IsFromMessageAssembly(t) &&
                                                       t.Namespace == "FileProcessing.Messages.Commands");
            conventionsBuilder.DefiningEventsAs(t => IsFromMessageAssembly(t) &&
                                                     t.Namespace == "FileProcessing.Messages.Events");
            conventionsBuilder.DefiningMessagesAs(t => IsFromMessageAssembly(t) &&
                                                       t.Namespace == "FileProcessing.Messages.InternalMessages");
        }

        private static bool IsFromMessageAssembly(Type t)
        {
            return t.Namespace != null
                   && !t.Namespace.StartsWith("NServiceBus.")
                   && !t.Namespace.StartsWith("System.");
        }
    }
}
