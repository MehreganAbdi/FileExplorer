using FileExplorer.DTOs;
using FileExplorer.Models;

namespace FileExplorer.IService
{
    public interface IDataTranformerService
    {
        FileDTO ChangeFileToFileDTO(File_ file);
        DirectoryDTO ChangeDirectoryToDirectoryDTO(Directory_ directory);
        
    }
}
