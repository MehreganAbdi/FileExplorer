using FileExplorer.Models;

namespace FileExplorer.IRepository
{
    public interface IFileEntityRepository
    {
        ICollection<FileEntity> GetAllByType(string type);
        Task<ICollection<FileEntity>> GetAllByTypeAsync(string type);

        ICollection<FileEntity> GetAll();
        ICollection<FileEntity> GetFilesByProjectId(int projectId);
        FileEntity GetById(int Id);
        Task<ICollection<FileEntity>> GetAllAsync();
        Task<ICollection<FileEntity>> GetFilesByProjectIdAsync(int projectId);
        Task<FileEntity> GetByIdAsync(int Id);
        bool AddFileEntity(FileEntity file);
        Task<bool> AddFileEntityAsync(FileEntity file);

        bool Update(FileEntity file);
        Task<bool> UpdateAsync(FileEntity file);
        bool RemoveFileEntity(FileEntity file);
        Task<bool> RemoveFileEntityAsync(FileEntity file);
        bool Save();
        Task<bool> SaveAsync();

    }
}
