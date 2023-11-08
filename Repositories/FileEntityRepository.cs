using FileExplorer.Data;
using FileExplorer.IRepository;
using FileExplorer.Models;
using Microsoft.EntityFrameworkCore;

namespace FileExplorer.Repositories
{
    public class FileEntityRepository : IFileEntityRepository
    {
        private readonly FileExplorerDbContext context;
        public FileEntityRepository(FileExplorerDbContext context)
        {
            this.context = context;            
        }

        public bool AddFileEntity(FileEntity file)
        {
            context.Files.Add(file);
            return Save();
         }

        public async Task<bool> AddFileEntityAsync(FileEntity file)
        {
            context.Files.AddAsync(file);
            return await SaveAsync();
        }

        public ICollection<FileEntity> GetAll()
        {
            return context.Files.ToList();
        }

        public async Task<ICollection<FileEntity>> GetAllAsync()
        {
            return await context.Files.ToListAsync();
        }

        public ICollection<FileEntity> GetAllByType(string type)
        {
            return context.Files.Where(f => f.Type == type).ToList();
        }

        public async Task<ICollection<FileEntity>> GetAllByTypeAsync(string type)
        {
            return await context.Files.Where(f => f.Type == type).ToListAsync();
        }

        public FileEntity GetById(int Id)
        {
            return context.Files.FirstOrDefault(f => f.Id == Id);
        }

        public FileEntity GetByIdAsNoTracking(int Id)
        {
            return context.Files.AsNoTracking().FirstOrDefault(f => f.Id == Id);
        }

        public async Task<FileEntity> GetByIdAsNoTrackingAsync(int Id)
        {
            return await context.Files.AsNoTracking().FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task<FileEntity> GetByIdAsync(int Id)
        {
            return await context.Files.FirstOrDefaultAsync(f => f.Id == Id);
        }

        public ICollection<FileEntity> GetFilesByProjectId(int projectId)
        {
            return context.Files.Where(f => f.ProjectId == projectId).ToList();
        }

        public async Task<ICollection<FileEntity>> GetFilesByProjectIdAsync(int projectId)
        {
            return await context.Files.Where(f => f.ProjectId == projectId).ToListAsync();
        }

        public bool RemoveFileEntity(FileEntity file)
        {
            context.Files.Remove(file);
            return Save();
        }

        public async Task<bool> RemoveFileEntityAsync(FileEntity file)
        {
            context.Files.Remove(file);
            return await SaveAsync();
        }

        public bool Save()
        {
            return context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool Update(FileEntity file)
        {
            context.Files.Update(file);
            return Save();
        }

        public async Task<bool> UpdateAsync(FileEntity file)
        {
            context.Files.Update(file);
            return await SaveAsync();
        }
    }
}
