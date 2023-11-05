using System.IO;
using FileExplorer.DTOs;
using FileExplorer.IService;
using FileExplorer.Models;
using FileExplorer.ViewModels;

namespace FileExplorer.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IDataTranformerService dataTranformerService;

        public DirectoryService(IDataTranformerService dataTranformerService)
        {
            this.dataTranformerService = dataTranformerService;
        }

        public async Task<FileExploreViewModel> SearchResult(string searching, FileExploreViewModel pathData)
        {

            pathData.Files = pathData.Files.Where(f => f.Name.Contains(searching) ||
                                                     f.path.Contains(searching) ||
                                                     f.Size.Contains(searching) ||
                                                     f.Type.Contains(searching) ||
                                                     f.CreatedDate.Contains(searching)).ToList();


            pathData.Directories = pathData.Directories.Where(f => f.Name.Contains(searching) ||
                                                  f.path.Contains(searching) ||
                                                  f.Size.Contains(searching) ||
                                                  f.Type.Contains(searching) ||
                                                  f.CreatedDate.Contains(searching)).ToList();
            return pathData;

        }
        public async Task<FileExploreViewModel> GetDataInViewModel(string path)
        {

            var data = await GetData(path);
            var dataVM = new FileExploreViewModel()
            {
                Directories = new List<DirectoryDTO>(),
                Files = new List<FileDTO>()
            };

            if (data.Files != null)
            {
                foreach (var file in data.Files)
                {
                    dataVM.Files.Add(dataTranformerService.ChangeFileToFileDTO(file));
                }
            }

            if (data.Directories != null)
            {
                foreach (var directory in data.Directories)
                {
                    dataVM.Directories.Add(dataTranformerService.ChangeDirectoryToDirectoryDTO(directory));
                }
            }

            return dataVM;

        }

        public async Task<FileExplore> GetData(string path)
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


        private File_[] GetFilesData(string[] files)
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
                        Size = GetFileSize(item).ToString() + " B",
                        CreatedDate = GetFileCreatedTime(item),
                        Type = GetTypeOfFile(item)
                    }
                    );
            }


            return items.ToArray();
        }


        private Directory_[] GetDirectoriesData(string[] directories)
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
                        Size = GetDirectorySize(item).ToString() + " B",
                        CreatedDate = GetDirectoryCreatedTime(item),
                        Type = "Folder"
                    }
                    );
            }


            return items.ToArray();
        }


        public string ConverViewModelTostring(FileExploreViewModel fileExploreViewModel)
        {
            var text = "";



            if (fileExploreViewModel.Files != null)
            {
                foreach (var file in fileExploreViewModel.Files)
                {
                    text += ("Name : " + file.Name + " | " + "Path : " + file.path + " | "
                        + "DateCreated : " + file.CreatedDate + " | " + "Size : " + file.Size + " | " +
                        "Type : " + file.Type + "\n,\n");
                }

            }

            if (fileExploreViewModel.Directories != null)
            {
                foreach (var file in fileExploreViewModel.Directories)
                {
                    text += ("Name : " + file.Name + " | " + "Path : " + file.path + " | "
                        + "DateCreated : " + file.CreatedDate + " | " + "Size : " + file.Size + " | " +
                        "Type : " + file.Type.ToString() + "\n,\n");
                }

            }

            return text;
        }
        public bool PathExists(string path)
        {
            return Directory.Exists(path);
        }


       

        public async Task<bool> NewFolder(string path , string? name = "NewFolder")
        {
            
            Directory.CreateDirectory(path+"\\"+name);
           
            return  PathExists(path + "\\" + name);
        }




        //Helper Methods

        private string GetName(string path)
        {
            var name = path.Split(@"\")[^1];

            if (name.Contains("."))
            {
                name = name.Split(".")[^2];
            }
            return name;
        }


        private long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }


        private long GetDirectorySize(string path)
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


        private string GetDirectoryCreatedTime(string path)
        {
            return Directory.GetCreationTime(path).ToString();
        }

        private string GetFileCreatedTime(string path)
        {
            return File.GetCreationTime(path).ToString();
        }

        private string GetTypeOfFile(string path)
        {
            return path.Split(".")[^1] == null ? "NotDefined" : path.Split(".")[^1];
        }

        public async Task<bool> AddFileToPath(string path,IFormFile file)
        {
            string filePath = path + "\\" + file.FileName;

            using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return PathExists(path + file.Name);
        }
    }
}
