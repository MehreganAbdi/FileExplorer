using System.ComponentModel.DataAnnotations;

namespace FileExplorer.Models
{
    public class Project
    {
        public int Id { get; set; }
        [MaxLength(100,ErrorMessage ="Can Be Approximately 100 Characters")]
        public string ProjectName { get; set; }
    }
}
