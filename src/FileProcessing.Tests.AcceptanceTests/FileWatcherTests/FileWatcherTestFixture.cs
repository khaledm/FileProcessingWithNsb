namespace FileProcessing.Tests.AcceptanceTests.FileWatcherTests
{
    using System;
    using Bogus;
    using FileWatcher.Handlers;
    using FileWatcher.Interfaces;
    using FileWatcher.Models;
    using Moq;
    using NServiceBus.Testing;

    public class FileWatcherFixture
    {
        public TestableMessageSession MessageSession { get; }
        public FileHandler SystemUnderTest { get; }
        public FileDetail TestFileDetails { get; }

        public FileWatcherFixture()
        {
            MessageSession = new TestableMessageSession();
            TestFileDetails = GetTestFileData();
            var fileDetailsProvider =
                Mock.Of<IProvideFileDetails>(details => details.GetFileInfo(It.IsAny<string>()) == TestFileDetails);
            SystemUnderTest = new FileHandler(MessageSession, fileDetailsProvider);
        }

        private FileDetail GetTestFileData()
        {
            return new Faker<FileDetail>()
                .RuleFor(o => o.FileName, f => $"FileName{DateTime.UtcNow.ToLongDateString()}")
                .RuleFor(o => o.FullFileName, f => $"c:\\FileLocation\\FileName{DateTime.UtcNow.ToLongDateString()}")
                .RuleFor(o => o.FileExtension, f => @".csv")
                .Generate();
        }
    }
}