﻿using FileExplorer.DTOs;
using FileExplorer.IRepository;
using FileExplorer.IService;

namespace FileExplorer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IDataTranformerService dataTranformerService;

        public ProjectService(IProjectRepository projectRepository,
                              IDataTranformerService dataTranformerService)
        {
            this.projectRepository = projectRepository;
            this.dataTranformerService = dataTranformerService;
        }

        public bool AddProject(ProjectDTO project)
        {
            return projectRepository.AddProject(dataTranformerService.ChangeProjectDTOToProject(project));
        }

        public async Task<bool> AddProjectAsync(ProjectDTO project)
        {
            return await projectRepository.AddProjectAsync(await dataTranformerService.ChangeProjectDTOToProjectAsync(project));

        }

        public ICollection<ProjectDTO> GetAll()
        {
            var projects = projectRepository.GetAll();
            var projectsDTOs = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                projectsDTOs.Add(dataTranformerService.ChangeProjectToProjectDTO(project));
            }

            return projectsDTOs;
        }

        public async Task<ICollection<ProjectDTO>> GetAllAsync()
        {
            var projects = await projectRepository.GetAllAsync();
            var projectsDTOs = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                projectsDTOs.Add(await dataTranformerService.ChangeProjectToProjectDTOAsync(project));
            }

            return projectsDTOs;

        }

        public ProjectDTO GetById(int Id)
        {
            return dataTranformerService.ChangeProjectToProjectDTO(projectRepository.GetById(Id));
        }

        public ProjectDTO GetByIdAsNoTracking(int Id)
        {
            return dataTranformerService.ChangeProjectToProjectDTO(projectRepository.GetById(Id));

        }

        public async Task<ProjectDTO> GetByIdAsNoTrackingAsync(int Id)
        {
            return await dataTranformerService.ChangeProjectToProjectDTOAsync(await projectRepository.GetByIdAsNoTrackingAsync(Id));

        }

        public async Task<ProjectDTO> GetByIdAsync(int Id)
        {
             return await dataTranformerService.ChangeProjectToProjectDTOAsync(await projectRepository.GetByIdAsync(Id));
        }

        public ProjectDTO GetProjectByName(string Name)
        {
            return dataTranformerService.ChangeProjectToProjectDTO(projectRepository.GetProjectByProjectName(Name));

        }

        public async Task<ProjectDTO> GetProjectByNameAsync(string Name)
        {
            return await dataTranformerService.ChangeProjectToProjectDTOAsync(await projectRepository.GetProjectByProjectNameAsync(Name));
        }

        public async Task<ICollection<ProjectDTO>> GetProjectsBysearch(string searchValue)
        {
            var projects = await projectRepository.GetProjectsBySearch(searchValue);
            var projectsDTOs = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                projectsDTOs.Add(await dataTranformerService.ChangeProjectToProjectDTOAsync(project));
            }

            return projectsDTOs;
        }

        public async Task<bool> ProjectExists(string name)
        {
            return await projectRepository.GetProjectByProjectNameAsync(name) != null ? true : false;
        }
        public bool RemoveProject(ProjectDTO project)
        {
            return projectRepository.RemoveProject(dataTranformerService.ChangeProjectDTOToProjectWithId(project));
        }

        public async Task<bool> RemoveProjectAsync(ProjectDTO project)
        {
            return await projectRepository.RemoveProjectAsync(await dataTranformerService.ChangeProjectDTOToProjectAsyncWithId(project));

        }

        public bool Update(ProjectDTO project)
        {
            return projectRepository.Update(dataTranformerService.ChangeProjectDTOToProjectWithId(project));
        }

        public async Task<bool> UpdateAsync(ProjectDTO project)
        {
            return await projectRepository.UpdateAsync(await dataTranformerService.ChangeProjectDTOToProjectAsyncWithId(project));

        }
    }
}
