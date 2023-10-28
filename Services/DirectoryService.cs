using System.IO;
using WebApplication.IService;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class DirectoryService : IDirectoryService
    {

        public  FileExplore GetData(string path)
        {
            var pathFiles = Directory.GetFiles(path);
            var pathDirectories = Directory.GetDirectories(path);


            var filesData = new List<File_>();
            var directoriesData = new List<Directory_>();


            if (pathFiles != null)
            {
                filesData = GetFilesData(pathFiles).ToList();
            }

            if (pathDirectories != null)
            {
                directoriesData = GetDirectoriesData(pathDirectories).ToList();
            }

            return new FileExplore()
            {
                Files = filesData,
                Directories = directoriesData
            };
        }

       
        private  File_[] GetFilesData(string[] files)
        {
            if (files == null)
            {
                return null;
            }

            var items = new List<File_>();

            foreach (var item in files)
            {
                items.Add(
                    new File_
                    {
                        Name = GetName(item),
                        path = item,
                        Size = GetFileSize(item).ToString() + " KB",
                        CreatedDate = GetFileCreatedTime(item),
                        Type = GetTypeOfFile(item)
                    }
                    );
            }


            return items.ToArray();
        }


        private  Directory_[] GetDirectoriesData(string[] directories)
        {
            if (directories == null)
            {
                return null;
            }

            var items = new List<Directory_>();

            foreach (var item in directories)
            {
                items.Add(
                    new Directory_
                    {
                        Name = GetName(item),
                        path = item,
                        Size = GetDirectorySize(item).ToString() + " KB",
                        CreatedDate = GetDirectoryCreatedTime(item),
                        Type = "Directory"
                    }
                    );
            }


            return items.ToArray();
        }



        //Helper Methods

        private  string GetName(string path)
        {
            var name = path.Split(@"\")[^1];

            if (name.Contains("."))
            {
                name = name.Split(".")[^2];
            }
            return name;
        }


        private  long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }


        private  long GetDirectorySize(string path)
        {
            long size = 0;

            var files = Directory.GetFiles(path);

            var subDirectories = Directory.GetDirectories(path);

            if (files != null)
            {
                foreach (var file in files)
                {
                    size += GetFileSize(file);
                }
            }

            if (subDirectories != null)
            {
                foreach (var directory in subDirectories)
                {
                    size += GetDirectorySize(directory);
                }
            }


            return size;
        }


        private  string GetDirectoryCreatedTime(string path)
        {
            return Directory.GetCreationTime(path).ToString();
        }

        private  string GetFileCreatedTime(string path)
        {
            return File.GetCreationTime(path).ToString();
        }

        private  string GetTypeOfFile(string path)
        {
            return path.Split(".")[^1] == null ? "NotDefined" : path.Split(".")[^1];
        }

    }
}
