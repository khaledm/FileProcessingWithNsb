namespace FileProcessing.Tests.AcceptanceTests.FileWatcherTests
{
    using System.Threading.Tasks;
    using FileProcessing.Messages.Commands;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class FileWatcherTests
    {
        // Given there are files in Ready folder location
        // And the files are valid (correct naming convention, correct file type)
        // And have been moved to Inprogress queue
        // When File Handler invoked to process with a file path location in InProgress folder
        // The File handler should  send a command message to file parser to start processing the file
        // And the file processing command message should include file's fully qualified name
        // And the file processing command message should include file's file type
        // [optional] And the file processing command message should include total record count

        [TestClass]
        public class When_File_Handler_Is_Invoked_To_Handle_A_File : FileWatcherTests
        {
            private FileWatcherFixture _testFixture;

            [TestInitialize]
            public async Task Setup()
            {
                _testFixture = new FileWatcherFixture();

                await _testFixture.SystemUnderTest.DoHandle(string.Empty).ConfigureAwait(false);
            }

            [TestMethod]
            public void Then_Should_Send_A_File_Processing_Command()
            {
                Assert.AreEqual(1, _testFixture.MessageSession.SentMessages.Length);
                Assert.IsInstanceOfType(_testFixture.MessageSession.SentMessages[0].Message, typeof(StartProcessingFile));
            }

            [TestMethod]
            public void Then_Sent_File_Processing_Command_Should_Have_Fully_Qualified_File_Name()
            {
                Assert.IsInstanceOfType(_testFixture.MessageSession.SentMessages[0].Message, typeof(StartProcessingFile));
                Assert.AreEqual(_testFixture.TestFileDetails.FullFileName,
                    ((StartProcessingFile)_testFixture.MessageSession.SentMessages[0].Message).FilePath);
            }

            [TestMethod]
            public void Then_Sent_File_Processing_Command_Should_Have_File_Name()
            {
                Assert.IsInstanceOfType(_testFixture.MessageSession.SentMessages[0].Message, typeof(StartProcessingFile));
                Assert.AreEqual(_testFixture.TestFileDetails.FileName,
                    ((StartProcessingFile)_testFixture.MessageSession.SentMessages[0].Message).FileName);
            }

            [TestMethod]
            public void Then_Sent_File_Processing_Command_Should_Have_File_Extension()
            {
                Assert.IsInstanceOfType(_testFixture.MessageSession.SentMessages[0].Message, typeof(StartProcessingFile));
                Assert.AreEqual(_testFixture.TestFileDetails.FileExtension,
                    ((StartProcessingFile)_testFixture.MessageSession.SentMessages[0].Message).FileType);
            }
        }
    }
}