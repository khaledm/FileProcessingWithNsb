using System.IO;
using System.Threading.Tasks;
using FileWatcher.Models;

namespace FileWatcher.Interfaces
{
    public interface IProvideFileDetails
    {
        FileDetail GetFileInfo(string filePath);
    }
}