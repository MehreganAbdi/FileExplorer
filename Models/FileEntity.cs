using System.ComponentModel.DataAnnotations;

namespace FileExplorer.Models
{
    public class FileEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string FilePath { get; set; }
        public int ProjectId { get; set; }
        public string Size { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; }

    }
}
