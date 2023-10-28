using WebApplication.Models;

namespace WebApplication.IService
{
    public interface IDirectoryService
    {
        FileExplore GetData(string path);

    }
}
