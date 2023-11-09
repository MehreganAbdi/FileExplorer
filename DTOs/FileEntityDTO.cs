using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FileExplorer.DTOs
{
    public class FileEntityDTO
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "Name Must Be Proximately 100 Characters")]
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [MaxLength(180, ErrorMessage = "File Path Can Be Approximately 180 Characters")]
        public string FilePath { get; set; }
        public int ProjectId { get; set; }
        public string Size { get; set; }
        [MaxLength(100, ErrorMessage = "Project Name Can Be Approximately 100 Characters")]
        public string ProjectName { get; set; }
        public DateTime DateCreated { get; set; }
        [MaxLength(15, ErrorMessage = "Can't Be More Than 15 Characters")]
        public string Type { get; set; }
        public IFormFile? FileToCopy { get; set; }
    }
}