using FileExplorer.DTOs;
using FileExplorer.Models;

namespace FileExplorer.IService
{
    public interface IProjectService
    {
        ICollection<ProjectDTO> GetAll();
        Task<ICollection<ProjectDTO>> GetAllAsync();
        ProjectDTO GetById(int Id);
        Task<ProjectDTO> GetByIdAsync(int Id);
        ProjectDTO GetByIdAsNoTracking(int Id);
        Task<ProjectDTO> GetByIdAsNoTrackingAsync(int Id);
        bool AddProject(ProjectDTO project);
        Task<bool> AddProjectAsync(ProjectDTO project);
        bool Update(ProjectDTO project);
        Task<bool> UpdateAsync(ProjectDTO project);
        bool RemoveProject(ProjectDTO project);
        Task<bool> RemoveProjectAsync(ProjectDTO project);
        ProjectDTO GetProjectByName(string Name);
        Task<ProjectDTO> GetProjectByNameAsync(string Name);

    }
}
