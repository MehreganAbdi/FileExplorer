using FileExplorer.Models;
using FileExplorer.ViewModels;

namespace FileExplorer.IService
{
    public interface IDirectoryService
    {
        Task<FileExplore> GetData(string path);
        Task<FileExploreViewModel> GetDataInViewModel(string path);
        string ConverViewModelTostring(FileExploreViewModel fileExploreViewModel);
        Task<FileExploreViewModel> SearchResult(string searching, FileExploreViewModel pathData);
        bool PathExists(string path);
        Task<bool> AddFileToPath(string path,IFormFile file);
        Task<bool> NewFolder(string path,string? name = "NewFolder");
        Task<bool> DownloadFileInDownloads(string path);
        void CreateLocalFile(string content);
        void DeleteLocalFile();
        bool DeleteFileByPath(string path);
        Task<byte[]> GetBytes(string path);
    }
}