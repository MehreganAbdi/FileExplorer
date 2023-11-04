using FileExplorer.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FileExplorer.ViewModels
{
    public class FileExploreViewModel
    {
        public List<FileDTO> Files{ get; set; }
        public List<DirectoryDTO> Directories{ get; set; }

        [Required]
        public string path { get; set; }
        public string? searching { get; set; }
        public string? NewFolderName { get; set; }
        public IFormFile? SelectedFile { get; set; }
    }
}
