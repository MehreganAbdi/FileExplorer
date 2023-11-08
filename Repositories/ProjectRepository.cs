using FileExplorer.Data;
using FileExplorer.IRepository;
using FileExplorer.Models; 
using Microsoft.EntityFrameworkCore;

namespace FileExplorer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly FileExplorerDbContext context;

        public ProjectRepository(FileExplorerDbContext context)
        {
            this.context = context;
        }
        public bool AddProject(Project project)
        {
            context.Projects.Add(project);
            return Save();
        }

        public async Task<bool> AddProjectAsync(Project project)
        {
            await context.Projects.AddAsync(project);
            return await SaveAsync();
        }

        public ICollection<Project> GetAll()
        {
            return context.Projects.ToList();
        }

        public async Task<ICollection<Project>> GetAllAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public Project GetById(int Id)
        {
            return context.Projects.FirstOrDefault(p => p.Id == Id);
        }

        public Project GetByIdAsNoTracking(int Id)
        {
            return context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == Id);
        }

        public async Task<Project> GetByIdAsNoTrackingAsync(int Id)
        {
            return await context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);

        }

        public async Task<Project> GetByIdAsync(int Id)
        {
            return await context.Projects.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public Project GetProjectByProjectName(string ProjectName)
        {
            return context.Projects.FirstOrDefault(p => p.ProjectName == ProjectName);
        }

        public Task<Project> GetProjectByProjectNameAsync(string ProjectName)
        {
            return context.Projects.FirstOrDefaultAsync(p => p.ProjectName == ProjectName);

        }

        public bool RemoveProject(Project project)
        {
            context.Projects.Remove(project);
            return Save();
        }

        public async Task<bool> RemoveProjectAsync(Project project)
        {
            context.Projects.Remove(project);
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

        public bool Update(Project project)
        {
            context.Projects.Update(project);
            return Save();
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            context.Projects.Update(project);
            return await SaveAsync();
        }
    }
}
