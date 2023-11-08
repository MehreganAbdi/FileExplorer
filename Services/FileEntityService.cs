using FileExplorer.DTOs;
using FileExplorer.IRepository;
using FileExplorer.IService;

namespace FileExplorer.Services
{
    public class FileEntityService : IFileEntityService
    {
        private readonly IFileEntityRepository fileEntityRepository;
        private readonly IDataTranformerService dataTranformerService;

        public FileEntityService(IFileEntityRepository fileEntityRepository,
                                 IDataTranformerService dataTranformerService)
        {
            this.fileEntityRepository = fileEntityRepository;
            this.dataTranformerService = dataTranformerService;
        }

        public bool AddFileEntity(FileEntityDTO file)
        {
            return fileEntityRepository.AddFileEntity(dataTranformerService.ChangeFileEntityDTOToFileEntity(file));
        }

        public async Task<bool> AddFileEntityAsync(FileEntityDTO file)
        {
            return await fileEntityRepository.AddFileEntityAsync(await dataTranformerService.ChangeFileEntityDTOToFileEntityAsync(file));

        }

        public ICollection<FileEntityDTO> GetAll()
        {
            var fileEntities = fileEntityRepository.GetAll();

            var fileEntitiesDTO = new List<FileEntityDTO>();

            foreach (var file in fileEntities)
            {
                fileEntitiesDTO.Add(dataTranformerService.ChangeFileEntityToFileEntityDTO(file));
            }

            return fileEntitiesDTO;
        }

        public async Task<ICollection<FileEntityDTO>> GetAllAsync()
        {
            var fileEntities = await fileEntityRepository.GetAllAsync();

            var fileEntitiesDTO = new List<FileEntityDTO>();

            foreach (var file in fileEntities)
            {
                fileEntitiesDTO.Add(await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(file));
            }

            return fileEntitiesDTO;
        }

        public ICollection<FileEntityDTO> GetAllByType(string type)
        {
            var fileEntities = fileEntityRepository.GetAllByType(type);

            var fileEntitiesDTO = new List<FileEntityDTO>();

            foreach (var file in fileEntities)
            {
                fileEntitiesDTO.Add(dataTranformerService.ChangeFileEntityToFileEntityDTO(file));
            }

            return fileEntitiesDTO;
        }

        public async Task<ICollection<FileEntityDTO>> GetAllByTypeAsync(string type)
        {
            var fileEntities = await fileEntityRepository.GetAllByTypeAsync(type);

            var fileEntitiesDTO = new List<FileEntityDTO>();

            foreach (var file in fileEntities)
            {
                fileEntitiesDTO.Add(await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(file));
            }

            return fileEntitiesDTO;
        }

        public FileEntityDTO GetById(int Id)
        {
            return dataTranformerService.ChangeFileEntityToFileEntityDTO(fileEntityRepository.GetById(Id));
        }

        public FileEntityDTO GetByIdAsNoTracking(int Id)
        {
            return dataTranformerService.ChangeFileEntityToFileEntityDTO(fileEntityRepository.GetByIdAsNoTracking(Id));

        }

        public async Task<FileEntityDTO> GetByIdAsNoTrackingAsync(int Id)
        {
            return await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(await fileEntityRepository.GetByIdAsNoTrackingAsync(Id));
        }


        public async Task<FileEntityDTO> GetByIdAsync(int Id)
        {
            return await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(await fileEntityRepository.GetByIdAsync(Id));
        }

       
        public ICollection<FileEntityDTO> GetFilesByProjectId(int projectId)
        {
            var files = fileEntityRepository.GetFilesByProjectId(projectId);

            var filesDTOs = new List<FileEntityDTO>();

            foreach (var file in files)
            {
                filesDTOs.Add(dataTranformerService.ChangeFileEntityToFileEntityDTO(file));
            }
            return filesDTOs;
        }

        public async Task<ICollection<FileEntityDTO>> GetFilesByProjectIdAsync(int projectId)
        {

            var files = await fileEntityRepository.GetFilesByProjectIdAsync(projectId);

            var filesDTOs = new List<FileEntityDTO>();

            foreach (var file in files)
            {
                filesDTOs.Add(await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(file));
            }
            return filesDTOs;
        }

        public bool RemoveFileEntity(FileEntityDTO file)
        {
            return fileEntityRepository.RemoveFileEntity(dataTranformerService.ChangeFileEntityDTOToFileEntityWithId(file));
        }

        public async Task<bool> RemoveFileEntityAsync(FileEntityDTO file)
        {
            return await fileEntityRepository.RemoveFileEntityAsync(await dataTranformerService.ChangeFileEntityDTOToFileEntityAsyncWithId(file));

        }

        public bool Update(FileEntityDTO file)
        {
            return fileEntityRepository.Update(dataTranformerService.ChangeFileEntityDTOToFileEntityWithId(file));
        }

        public async Task<bool> UpdateAsync(FileEntityDTO file)
        {
            return await fileEntityRepository.UpdateAsync(await dataTranformerService.ChangeFileEntityDTOToFileEntityAsyncWithId(file));

        }
    }
}
