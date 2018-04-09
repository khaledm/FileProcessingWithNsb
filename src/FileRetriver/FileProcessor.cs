namespace FileRetriver
{
    using System;
    using System.Threading.Tasks;
    using FileProcessing.Messages.Commands;
    using NServiceBus;
    using NServiceBus.Logging;

    public class FileProcessorSaga : Saga<FileProcessorSaga.SagaData>,
        IAmStartedByMessages<StartProcessingFile>,
        IHandleTimeouts<FileProcessorSaga.TimeoutState>
    {
        private static ILog _log = LogManager.GetLogger<FileProcessorSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<StartProcessingFile>(m => m.ImportId).ToSaga(m => m.ImportId);
        }

        public async Task Handle(StartProcessingFile message, IMessageHandlerContext context)
        {
            if (Data.HasStarted)
            {
                _log.Info("Saga has started");
                return;
            }

            _log.Info("Hello from FileProcessor handler");
            _log.InfoFormat("Processing file '{0}' from location '{1}', TotalRecordCount {2}", message.FileName,
                message.FilePath, message.TotalRecordCount);

            await PopulateSagaData(message);

            // Start processing by retrieving the file
            // breaking the content into chunks
            // process each chunk
            // move the file into "processed" folder

            MarkAsComplete();

            _log.Warn("Saga Complete");

            await Task.CompletedTask;
        }

        private async Task PopulateSagaData(StartProcessingFile startingMessage)
        {
            Data.HasStarted = true;
            Data.ImportId = startingMessage.ImportId;
            Data.FileName = startingMessage.FileName;
            Data.FilePath = startingMessage.FilePath;
            Data.TotalRecordCount = startingMessage.TotalRecordCount;

            await Task.CompletedTask;
        }

        public Task Timeout(TimeoutState state, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public class SagaData : IContainSagaData
        {
            public Guid Id { get; set; }
            public string Originator { get; set; }
            public string OriginalMessageId { get; set; }
            public Guid ImportId { get; set; }
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public int TotalRecordCount { get; set; }
            public bool HasStarted { get; set; }
        }

        public class TimeoutState
        {
        }
    }
}