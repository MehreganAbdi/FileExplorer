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

        public FileEntity ChangeFileEntityDTOToFileEntity(FileEntityDTO fileDTO)
        {
            return new FileEntity()
            {
                Name = fileDTO.Name,
                DateCreated = fileDTO.DateCreated,
                Size = fileDTO.Size,
                ProjectName = fileDTO.ProjectName,

                Description = fileDTO.Description,
                FilePath = fileDTO.FilePath,
                ProjectId = fileDTO.ProjectId,
                Type = fileDTO.Type,
                ImageLink = fileDTO.ImageLink
            };
        }

        public async Task<FileEntity> ChangeFileEntityDTOToFileEntityAsync(FileEntityDTO fileDTO)
        {
            return new FileEntity()
            {
                Name = fileDTO.Name,
                DateCreated = fileDTO.DateCreated,
                Size = fileDTO.Size,
                Description = fileDTO.Description,
                FilePath = fileDTO.FilePath,
                ProjectId = fileDTO.ProjectId,
                ProjectName = fileDTO.ProjectName,
                ImageLink = fileDTO.ImageLink,
                Type = fileDTO.Type
            };
        }

        public async Task<FileEntity> ChangeFileEntityDTOToFileEntityAsyncWithId(FileEntityDTO fileDTO)
        {
            return new FileEntity()
            {
                Name = fileDTO.Name,
                DateCreated = fileDTO.DateCreated,
                Size = fileDTO.Size,
                Description = fileDTO.Description,
                FilePath = fileDTO.FilePath,
                ProjectId = fileDTO.ProjectId,
                ProjectName = fileDTO.ProjectName,
                ImageLink = fileDTO.ImageLink,
                Type = fileDTO.Type,
                Id = fileDTO.Id
            };
        }

        public FileEntity ChangeFileEntityDTOToFileEntityWithId(FileEntityDTO fileDTO)
        {
            return new FileEntity()
            {
                Name = fileDTO.Name,
                DateCreated = fileDTO.DateCreated,
                Size = fileDTO.Size,
                Description = fileDTO.Description,
                FilePath = fileDTO.FilePath,
                ProjectId = fileDTO.ProjectId,
                ProjectName = fileDTO.ProjectName,
                Type = fileDTO.Type,
                Id = fileDTO.Id,
                ImageLink = fileDTO.ImageLink
            };

        }

        public FileEntityDTO ChangeFileEntityToFileEntityDTO(FileEntity file)
        {
            return new FileEntityDTO()
            {
                Name = file.Name,
                Size = file.Size,
                DateCreated = file.DateCreated,
                Description = file.Description,
                FilePath = file.FilePath,
                ProjectId = file.ProjectId,
                Type = file.Type,
                Id = file.Id,
                ProjectName = file.ProjectName,
                ImageLink = file.ImageLink
            };
        }


        public async Task<FileEntityDTO> ChangeFileEntityToFileEntityDTOAsync(FileEntity file)
        {
            return new FileEntityDTO()
            {
                Name = file.Name,
                Size = file.Size,
                DateCreated = file.DateCreated,
                Description = file.Description,
                FilePath = file.FilePath,
                ProjectId = file.ProjectId,
                ProjectName = file.ProjectName,
                Type = file.Type,
                Id = file.Id,
                ImageLink = file.ImageLink 
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

        public Project ChangeProjectDTOToProject(ProjectDTO projectDTO)
        {
            return new Project()
            {
                ProjectName = projectDTO.ProjectName
            };
        }

        public async Task<Project> ChangeProjectDTOToProjectAsync(ProjectDTO projectDTO)
        {
            return new Project()
            {
                ProjectName = projectDTO.ProjectName,
            };
        }

        public async Task<Project> ChangeProjectDTOToProjectAsyncWithId(ProjectDTO projectDTO)
        {
            return new Project()
            {
                ProjectName = projectDTO.ProjectName,
                Id = projectDTO.Id
            };
        }

        public Project ChangeProjectDTOToProjectWithId(ProjectDTO projectDTO)
        {
            return new Project()
            {
                ProjectName = projectDTO.ProjectName,
                Id = projectDTO.Id
            };
        }

        public ProjectDTO ChangeProjectToProjectDTO(Project project)
        {
            return new ProjectDTO()
            {
                ProjectName = project.ProjectName,
                Id = project.Id
            };
        }

        public async Task<ProjectDTO> ChangeProjectToProjectDTOAsync(Project project)
        {
            return new ProjectDTO()
            {
                ProjectName = project.ProjectName,
                Id = project.Id
            };

        }
    }
}
