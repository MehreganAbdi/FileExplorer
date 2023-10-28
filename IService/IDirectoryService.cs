using FileExplorer.Models;

namespace FileExplorer.IService
{
    public interface IDirectoryService
    {
        Task<FileExplore> GetData(string path);

    }
}
