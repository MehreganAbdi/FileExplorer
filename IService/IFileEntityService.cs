using FileExplorer.DTOs;
using FileExplorer.Models;

namespace FileExplorer.IService
{
    public interface IFileEntityService
    {
        ICollection<FileEntityDTO> GetAllByType(string type);
        Task<ICollection<FileEntityDTO>> GetAllByTypeAsync(string type);

        ICollection<FileEntityDTO> GetAll();
        Task<ICollection<FileEntityDTO>> GetAllAsync();
        ICollection<FileEntityDTO> GetFilesByProjectId(int projectId);
        Task<ICollection<FileEntityDTO>> GetFilesByProjectIdAsync(int projectId);
        FileEntityDTO GetById(int Id);
        Task<FileEntityDTO> GetByIdAsync(int Id);
        FileEntityDTO GetByIdAsNoTracking(int Id);
        Task<FileEntityDTO> GetByIdAsNoTrackingAsync(int Id);
        bool AddFileEntity(FileEntityDTO file);
        Task<bool> AddFileEntityAsync(FileEntityDTO file);

        bool Update(FileEntityDTO file);
        Task<bool> UpdateAsync(FileEntityDTO file);
        bool RemoveFileEntity(FileEntityDTO file);
        Task<bool> RemoveFileEntityAsync(FileEntityDTO file);

    }
}
