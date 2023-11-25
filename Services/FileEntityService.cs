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

        public string ChangeBytesToString(byte[] fileInBytes)
        {
            var result = "";
            foreach (byte b in fileInBytes)
            {
                result += $",{b}";
            }


            return result;
        }

        public byte[] ChangeStringToByte(string fileInString)
        {
            var strings = fileInString.Split(",").ToArray();
            var result = new List<byte>();

            foreach (var str in strings)
            {
                result.Add(Convert.ToByte(str));
            }

            return result.ToArray();
        
        }

        public async Task<FileEntityDTO> CreateFileEntityDTODirectly(IFormFile file, FileEntityDTO fileEntityDTO)
        {
            return new FileEntityDTO()
            {
                FilePath = file.FileName,
                DateCreated = DateTime.Now,
                Description = fileEntityDTO.Description,
                Name = file.FileName.Split(".")[^2] != null? file.FileName.Split(".")[^2] : "Record",
                ProjectId = fileEntityDTO.ProjectId,
                ProjectName = fileEntityDTO.ProjectName,
                Size = file.Length.ToString()+"B",
                FileToCopy = file,
                Type = file.ContentType
            };
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

        

        public List<string> LastFivePaths()
        {
            return fileEntityRepository.LastFivePaths();
        }


        public bool RemoveFileEntity(FileEntityDTO file)
        {
            return fileEntityRepository.RemoveFileEntity(dataTranformerService.ChangeFileEntityDTOToFileEntityWithId(file));
        }

        public async Task<bool> RemoveFileEntityAsync(FileEntityDTO file)
        {
            return await fileEntityRepository.RemoveFileEntityAsync(await dataTranformerService.ChangeFileEntityDTOToFileEntityAsyncWithId(file));

        }

        public async Task<ICollection<FileEntityDTO>> SearchInRecords(string search)
        {
            var files =await fileEntityRepository.SearchInRecords(search);
            var fileEntitiesDTO = new List<FileEntityDTO>();

            foreach (var file in files)
            {
                fileEntitiesDTO.Add(await dataTranformerService.ChangeFileEntityToFileEntityDTOAsync(file));
            }

            return fileEntitiesDTO;
        }

        public bool Update(FileEntityDTO file)
        {
            return fileEntityRepository.Update(dataTranformerService.ChangeFileEntityDTOToFileEntityWithId(file));
        }

        public async Task<bool> UpdateAsync(FileEntityDTO file)
        {
            return fileEntityRepository.Update(await dataTranformerService.ChangeFileEntityDTOToFileEntityAsyncWithId(file));

        }
    }
}
