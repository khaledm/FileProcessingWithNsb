using System;
using System.Threading.Tasks;
using FileProcessing.Messages.Commands;
using FileWatcher.Interfaces;
using NServiceBus;

namespace FileWatcher.Handlers
{
    public class FileHandler : IHandleReadyFile
    {
        private readonly IMessageSession _session;
        private readonly IProvideFileDetails _fileDetailsProvider;

        public FileHandler(IMessageSession session, IProvideFileDetails fileDetailsProvider)
        {
            _session = session;
            _fileDetailsProvider = fileDetailsProvider;
        }

       public async Task DoHandle(string filePath)
       {
           var fileToProcess = _fileDetailsProvider.GetFileInfo(filePath);
           await _session.Send(new StartProcessingFile
           {
               ImportId = Guid.NewGuid(),
               FileName = fileToProcess.FileName,
               FilePath = fileToProcess.FullFileName,
               FileType = fileToProcess.FileExtension,
               //TotalRecordCount = totalRecordCount
           });
       }
    }
}