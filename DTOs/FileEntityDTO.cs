namespace FileExplorer.DTOs
{
    public class FileEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string FilePath { get; set; }
        public int ProjectId { get; set; }
        public string Size { get; set; }
        public string ProjectName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; }

    }
}
