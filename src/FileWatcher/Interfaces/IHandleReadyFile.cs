using System.Threading.Tasks;

namespace FileWatcher.Interfaces
{
    public interface IHandleReadyFile
    {
        Task DoHandle(string filePath);
    }
}