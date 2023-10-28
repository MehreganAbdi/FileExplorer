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
    }
}
