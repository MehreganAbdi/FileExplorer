using FileExplorer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileExplorer.Data
{
    public class FileExplorerDbContext : DbContext
    {
        public FileExplorerDbContext(DbContextOptions<FileExplorerDbContext> options) : base(options)
        {
            
        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<Project> Projects { get; set; }


    }
}
