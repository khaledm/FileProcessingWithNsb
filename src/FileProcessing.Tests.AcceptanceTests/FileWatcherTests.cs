using NServiceBus.Testing;

namespace FileProcessing.Tests.AcceptanceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    public class FileWatcherTests
    {
        // File watcher "watches" a file location called "Ready" for incoming files
        // When a "new" file lands in the "Ready" file location
        // Then it moves the file to "InProgress" file location
        // And it sends a command to start processing the file to File Processor

        [TestClass]
        public class WhenNewFileArrives
        {
            [TestMethod]
            public void Then_New_File_Should_Be_Moved_To_In_Progress()
            {
                //
            }

            [TestMethod]
            public void Then_Processing_Of_New_File_Should_Be_Started()
            {

            }
        }
    }
}