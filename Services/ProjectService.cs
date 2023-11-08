using FileExplorer.DTOs;
using FileExplorer.IRepository;
using FileExplorer.IService;

namespace FileExplorer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public bool AddProject(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddProjectAsync(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProjectDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProjectDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ProjectDTO GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ProjectDTO GetByIdAsNoTracking(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO> GetByIdAsNoTrackingAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProject(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProjectAsync(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProjectDTO project)
        {
            throw new NotImplementedException();
        }
    }
}
