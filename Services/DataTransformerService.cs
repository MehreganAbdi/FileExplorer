using FileExplorer.DTOs;
using FileExplorer.IService;
using FileExplorer.Models;
using System.IO;

namespace FileExplorer.Services
{
    public class DataTransformerService : IDataTranformerService
    {
        public DirectoryDTO ChangeDirectoryToDirectoryDTO(Directory_ directory)
        {
            return new DirectoryDTO()
            {
                Name = directory.Name,
                Size = directory.Size,
                CreatedDate = directory.CreatedDate,
                path = directory.path,
                Type = directory.Type
            };
        }

        public FileDTO ChangeFileToFileDTO(File_ file)
        {
            return new FileDTO()
            {
                Name = file.Name,
                Size = file.Size,
                CreatedDate = file.CreatedDate,
                path = file.path,
                Type = file.Type

            };
        }
    }
}
