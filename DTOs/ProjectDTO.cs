using System.ComponentModel.DataAnnotations;

namespace FileExplorer.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "Can Be Approximately 100 Characters")]
        public string ProjectName { get; set; }
        public string? CreateErrorTD { get; set; }
        public string? Error { get; set; }
        public string? EditErrorTD { get; set; }
        public string? DeleteErrorTD { get; set; }
    }
}
