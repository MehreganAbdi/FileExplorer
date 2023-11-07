using FileExplorer.Models;

namespace FileExplorer.IRepository
{
    public interface IProjectRepository
    {
        ICollection<Project> GetAll();
        Task<ICollection<Project>> GetAllAsync();
        Project GetById(int Id);
        Task<Project> GetByIdAsync(int Id);
        Project GetByIdAsNoTracking(int Id);
        Task<Project> GetByIdAsNoTrackingAsync(int Id);
        bool AddProject(Project project);
        Task<bool> AddProjectAsync(Project project);
        bool Update(Project project);
        Task<bool> UpdateAsync(Project project);
        bool RemoveProject(Project project);
        Task<bool> RemoveProjectAsync(Project project);

    }
}
