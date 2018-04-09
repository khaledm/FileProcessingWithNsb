namespace FileProcessing.Tests.AcceptanceTests
{
    using System;
    using System.Threading.Tasks;
    using Bogus;
    using FileProcessing.Messages.Commands;
    using FileRetriver;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NServiceBus.Testing;

    [TestClass]
    public class FileProcessorTests
    {
        // When a File Processor receives command to start processing a file
        // Then the File Processor retrieves the file from the In Progress location
        // Then the Processor start the processing saga

        [TestMethod]
        public async Task TestSagaDataAsync()
        {
            // arrange
            Randomizer.Seed = new Random(8675309);
            var data = new FileProcessorSaga.SagaData();

            var saga = new FileProcessorSaga
            {
                Data = data
            };

            var context = new TestableMessageHandlerContext();
            var sagaStarterMessage = new Faker<StartProcessingFile>()
                .RuleFor(o => o.FileName, f => $"FileName{DateTime.UtcNow.ToLongDateString()}")
                .RuleFor(o => o.FilePath, f => $"c:\\FileLocation\\{DateTime.UtcNow.ToLongDateString()}")
                .RuleFor(o => o.TotalRecordCount, f => f.Random.Number(100, 10000)).Generate();

            // act
            await saga.Handle(sagaStarterMessage, context).ConfigureAwait(false);

            // assert
            Assert.AreEqual(saga.Data.FileName, sagaStarterMessage.FileName);
            Assert.AreEqual(saga.Data.FilePath, sagaStarterMessage.FilePath);
            Assert.AreEqual(saga.Data.TotalRecordCount, sagaStarterMessage.TotalRecordCount);
            Assert.AreEqual(true, saga.Data.HasStarted);
        }
    }
}