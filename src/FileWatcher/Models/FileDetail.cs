namespace FileWatcher.Models
{
    public sealed class FileDetail
    {
        public string FileName { get; set; }
        /// <summary>
        /// File name with location
        /// </summary>
        public string FullFileName { get; set; }

        public string FileExtension { get; set; }
    }
}