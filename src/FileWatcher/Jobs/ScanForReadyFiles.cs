using System;

namespace FileWatcher.Jobs
{
    using System.Threading.Tasks;
    using Quartz;
    using NServiceBus;
    using System.IO;
    using System.Linq;
    using FileProcessing.Messages.Commands;
using NServiceBus.Logging;
using Serilog;

    public class ScanForReadyFiles : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger<ScanForReadyFiles>();

        public async Task Execute(IJobExecutionContext context)
        {
            var messageSession = context.MessageSession();

            // SCAN the file Ready location

            // loop through the unique file names and send them to In Progress file location

            // send commands to start processing of the files

            var filesFound = Directory.GetFiles(context.ReadyFolderPath(), "*.txt").ToList();

            foreach (var fileWithFullPath in filesFound.Where(path => !string.IsNullOrWhiteSpace(path)))
            {
                Log.InfoFormat("File found {0} ", fileWithFullPath);

                var totalRecordCount = GetRecordCount(fileWithFullPath);

                var fileName = Path.GetFileName(fileWithFullPath);

                Directory.Move(fileWithFullPath, Path.Combine(context.InProgressFolderPath(), fileName));

                var filePathInProgress = Path.Combine(context.InProgressFolderPath(), fileName);

                await messageSession.Send(
                        new StartProcessingFile
                        {
                            ImportId = Guid.NewGuid(),
                            FileName = fileName,
                            FilePath = filePathInProgress,
                            TotalRecordCount = totalRecordCount

                        })

                    .ConfigureAwait(false);

                Log.InfoFormat("Sent for processing {0} {1}", fileName, filePathInProgress);
            }
        }

        public int GetRecordCount(string fileWithFullPath)
        {
            using (var reader = File.OpenText(fileWithFullPath))
            {
                try
                {
                    var csv = new CsvHelper.CsvReader(reader);
                    csv.Configuration.Delimiter = @"|";
                    var records = csv.GetRecords(typeof(object));
                    return records.Skip(0).Count();
                }
                finally
                {
                    reader.Close();
                }
            }
        }
    }
}