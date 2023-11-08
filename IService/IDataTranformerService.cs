using FileExplorer.DTOs;
using FileExplorer.Models;

namespace FileExplorer.IService
{
    public interface IDataTranformerService
    {
        FileDTO ChangeFileToFileDTO(File_ file);
        DirectoryDTO ChangeDirectoryToDirectoryDTO(Directory_ directory);



        FileEntityDTO ChangeFileEntityToFileEntityDTO(FileEntity file);
        Task<FileEntityDTO> ChangeFileEntityToFileEntityDTOAsync(FileEntity file);
        
        FileEntity ChangeFileEntityDTOToFileEntity(FileEntityDTO fileDTO);
        Task<FileEntity> ChangeFileEntityDTOToFileEntityAsync(FileEntityDTO fileDTO);
        FileEntity ChangeFileEntityDTOToFileEntityWithId(FileEntityDTO fileDTO);
        Task<FileEntity> ChangeFileEntityDTOToFileEntityAsyncWithId(FileEntityDTO fileDTO);

        ProjectDTO ChangeProjectToProjectDTO(Project project);
        Task<ProjectDTO> ChangeProjectToProjectDTOAsync(Project project);

        Project ChangeProjectDTOToProject(ProjectDTO projectDTO);
        Task<Project> ChangeProjectDTOToProjectAsync(ProjectDTO projectDTO);
        Project ChangeProjectDTOToProjectWithId(ProjectDTO projectDTO);
        Task<Project> ChangeProjectDTOToProjectAsyncWithId(ProjectDTO projectDTO);
    }
}
