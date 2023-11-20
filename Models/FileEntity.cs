using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileExplorer.Models
{
    public class FileEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100,ErrorMessage ="Name Must Be Proximately 100 Characters")]
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [MaxLength(180,ErrorMessage = "File Path Can Be Approximately 180 Characters")]
        public string FilePath { get; set; }
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        [MaxLength(100,ErrorMessage ="Project Name Can Be Approximately 100 Characters")]
        public string ProjectName { get; set; }
        public string Size { get; set; }
        public DateTime DateCreated { get; set; }
        [MaxLength(15,ErrorMessage="Can't Be More Than 15 Characters")]
        public string Type { get; set; }

        public string? ImageLink { get; set; }

    }
}
