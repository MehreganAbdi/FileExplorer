using WebApplication.DTOs;

namespace WebApplication.ViewModels
{
    public class FileExploreViewModel
    {
        public List<FileDTO> Files{ get; set; }
        public List<DirectoryDTO> Directories{ get; set; }
    }
}
