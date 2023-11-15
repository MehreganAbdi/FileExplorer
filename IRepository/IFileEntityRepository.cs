using FileExplorer.Models;

namespace FileExplorer.IRepository
{
    public interface IFileEntityRepository
    {
        ICollection<FileEntity> GetAllByType(string type);
        Task<ICollection<FileEntity>> GetAllByTypeAsync(string type);

        ICollection<FileEntity> GetAll();
        Task<ICollection<FileEntity>> GetAllAsync();
        ICollection<FileEntity> GetFilesByProjectId(int projectId);
        Task<ICollection<FileEntity>> GetFilesByProjectIdAsync(int projectId);
        FileEntity GetById(int Id);
        Task<FileEntity> GetByIdAsync(int Id);
        FileEntity GetByIdAsNoTracking(int Id);
        Task<FileEntity> GetByIdAsNoTrackingAsync(int Id);
        bool AddFileEntity(FileEntity file);
        Task<bool> AddFileEntityAsync(FileEntity file);

        bool Update(FileEntity file);
        Task<bool> UpdateAsync(FileEntity file);
        bool RemoveFileEntity(FileEntity file);
        Task<bool> RemoveFileEntityAsync(FileEntity file);

        bool Save();
        Task<bool> SaveAsync();
        List<string> LastFivePaths();
    }
}
