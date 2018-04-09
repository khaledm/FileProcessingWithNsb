namespace FileProcessing.Messages.Commands
{
    using System;

    public class StartProcessingFile
    {
        public Guid ImportId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int TotalRecordCount { get; set; }

        public string FileType { get; set; }
    }
}