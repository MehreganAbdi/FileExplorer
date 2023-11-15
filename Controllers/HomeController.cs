using FileExplorer.Data.structs;
using FileExplorer.DTOs;
using FileExplorer.IService;

using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;
        private readonly IEmailService emailService;
        private readonly IFileEntityService fileEntityService;
        private readonly IProjectService projectService;

        public HomeController(IDirectoryService directoryService,
                              IEmailService emailService,
                              IFileEntityService fileEntityService,
                              IProjectService projectService)
        {
            this.directoryService = directoryService;
            this.emailService = emailService;
            this.fileEntityService = fileEntityService;
            this.projectService = projectService;
        }

        public async Task<IActionResult> Index(string path, string searching)
        {

            try
            {
                if (path == null || !directoryService.PathExists(path))
                {
                    return View(new FileExploreViewModel()
                    {
                        Error = "Insert An Existing Path"
                    });
                }

                var pathData = await directoryService.GetDataInViewModel(path);

                pathData.path = path;

                if (pathData == null)
                {
                    return NotFound();
                }


                if (searching != null)
                {
                    pathData.searching = searching;

                    pathData = await directoryService.SearchResult(searching, pathData);
                    if (pathData == null)
                    {

                        return View(new FileExploreViewModel()
                        {
                            Error = "Nothing Found"
                        });
                    }

                }


                return View(pathData);
            }
            catch (Exception ex)
            {
                
                return View(new FileExploreViewModel()
                {
                    Error = $"Error Occured ({ex.Message}), Try Again "
                });
            }
        }

        public async Task<FileResult> SaveFile(string path, string searching)
        {
            var pathData = await directoryService.GetDataInViewModel(path);
            if (searching != null)
            {

                pathData = await directoryService.SearchResult(searching, pathData);
            }
            string dataString = directoryService.ConverViewModelTostring(pathData);

            return File(System.Text.Encoding.UTF8.GetBytes(dataString), "text/xml", "FileExplorerData.txt");
        }





        public async Task<IActionResult> NewFolder(string path, string? NewFolderName = "NewFolder")
        {

            try
            {
                if (path == null || !directoryService.PathExists(path))
                {
                    return View(new FileExploreViewModel()
                    {
                        Error = "Path Didn't Exist , Check And Try Again"
                    });
                }
                await directoryService.NewFolder(path, NewFolderName);
                
                var redirectedFileExploreViewModel = await directoryService.GetDataInViewModel(path);
               
                
                redirectedFileExploreViewModel.AddResultTD = NewFolderName == null ? "NewFolder Created Successfuly In \n  >" + path.ToString() :
                                                               $"{NewFolderName} Created Successfuly In \n  >" + path.ToString();
                
                redirectedFileExploreViewModel.path = path;

                
                return View("Index", redirectedFileExploreViewModel);
            }
            catch (Exception ex)
            {
                var viewModel = await directoryService.GetDataInViewModel(path);
                viewModel.Error = ex.Message;
                return View("Index",viewModel);

            }

        }







        public async Task<IActionResult> EmailListResult(FileExploreViewModel fileExploreViewModel)
        {
            var newFileExploreViewModel = await directoryService.GetDataInViewModel(fileExploreViewModel.path);
            if (fileExploreViewModel.Reciever == null)
            {
                newFileExploreViewModel.Error = "To Send You Results Through Email , You Need To Fill Email Address Field";

            }
            else
            {

                var data = await directoryService.GetDataInViewModel(fileExploreViewModel.path);
                directoryService.CreateLocalFile(directoryService.ConverViewModelTostring(data));

                var emailDTO = new EmailDTO()
                {
                    Reciever = fileExploreViewModel.Reciever,
                    Subject = "Your Requested File",
                    message = "File : \n"
                };



                await emailService.SendFileByEmail(emailDTO, "G:\\Downloads\\FileData.txt");

            }
          

            newFileExploreViewModel.path = fileExploreViewModel.path;

            return View("Index", newFileExploreViewModel);
        }


        public async Task<IActionResult> Delete(string bothpath)
        {
            try
            {

                directoryService.DeleteFileByPath(bothpath.Split("&&&")[^2]);
                var fileExploreViewModel = await directoryService.GetDataInViewModel(bothpath.Split("&&&")[^1]);
                fileExploreViewModel.path = bothpath.Split("&&&")[^1];

                return View("Index", fileExploreViewModel);
            }
            catch (Exception ex)
            {
               
                var fileExploreViewModel = await directoryService.GetDataInViewModel(bothpath.Split("&&&")[^1]);
                
                fileExploreViewModel.path = bothpath.Split("&&&")[^1];
                
                fileExploreViewModel.Error = ex.Message;
                
                return View("Index", fileExploreViewModel);
            }
        }

        



        public async Task<FileResult> Download(string path, string type)
        {

            var bytes = await directoryService.GetBytes(path);

            if (type == "jpg" || type == "png" || type == "gif")
            {
                return File(bytes, "image/jpg", "FileExploreDownload." + type);
            }

            return File(bytes, "text/xml", "FileExploreDownload." + type);


        }


       

        [HttpPost]
        public async Task<IActionResult> AddFilesToPath(FileExploreViewModel fileExploreViewModel)
        {


            try
            {
                if (fileExploreViewModel.SelectedFile == null)
                {
                   fileExploreViewModel.SelectErrorTD = "Select A File First";

                    return RedirectToAction("Index", fileExploreViewModel);
                }


                var typo = fileExploreViewModel.SelectedFile.ContentType;

                if (typo != "image/jpg" && typo != "image/png" && typo != "image/jpeg")
                {
                    fileExploreViewModel.SelectErrorTD = "Selected File Must Be img";
                    return View("Index", fileExploreViewModel);
                }



               

                var fileEntityDTO = new FileEntityDTO()
                {
                    ProjectId = 13,
                    ProjectName = "NotDefined",
                    Type = typo,
                    DateCreated = directoryService.GetCreationTime(fileExploreViewModel.path+"\\"+ fileExploreViewModel.SelectedFile.FileName),
                    Size = fileExploreViewModel.SelectedFile.Length + "B",
                    Description = "NotDefined",
                    FilePath = fileExploreViewModel.path,
                    Name = fileExploreViewModel.SelectedFile.FileName.Split(".")[^2],
                    FileToCopy = fileExploreViewModel.SelectedFile
                };

                var allProjects = new ProjectsStruct(){AllProjects = await projectService.GetAllAsync()}; 

                ViewBag.data = allProjects;
                return View("Create", fileEntityDTO);


            }
            catch (Exception ex)
            {
                fileExploreViewModel.Error = ex.Message.ToString();
                return View("Create", fileExploreViewModel);

            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var allProjects = new ProjectsStruct(){AllProjects = await projectService.GetAllAsync()}; 

            ViewBag.data = allProjects;



            var reloadSafety = new FileEntityDTO();
            return View(reloadSafety);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FileEntityDTO fileEntityDTO)
        {
            var allProjects = new ProjectsStruct(){AllProjects = await projectService.GetAllAsync()}; 

            ViewBag.data = allProjects;

            try
            {
                if (!ModelState.IsValid)
                {
                    fileEntityDTO.CreateErrorTD = "Make Sure That You Filled All Gaps";

                    return View(fileEntityDTO);
                }
                else if (!await directoryService.ValidatePathPattern(fileEntityDTO.FilePath) )
                {
                    fileEntityDTO.NamingErrorTD = " Path Is Unvalid";
                    return View(fileEntityDTO);
                }
                else if(!await directoryService.ValidateNamePattern(fileEntityDTO.Name))
                {
                    fileEntityDTO.NamingErrorTD = " FileName Is Unvalid";
                    return View(fileEntityDTO);

                }

                await directoryService.AddFileToPath(fileEntityDTO.FilePath, fileEntityDTO.FileToCopy);


                fileEntityDTO.ProjectName = (await projectService.GetByIdAsync(fileEntityDTO.ProjectId)).ProjectName;


                await fileEntityService.AddFileEntityAsync(fileEntityDTO);


                

                var redirectedFileExploreViewModel = await directoryService.GetDataInViewModel(fileEntityDTO.FilePath);

                redirectedFileExploreViewModel.path = fileEntityDTO.FilePath;

                redirectedFileExploreViewModel.AddResultTD = "File Successfully Added To \n  >" + fileEntityDTO.FilePath.ToString();

                return View("Index", redirectedFileExploreViewModel);
            }
            catch (Exception ex)
            {
               
                ViewBag.data = allProjects;

                fileEntityDTO.Error = ex.Message.ToString();

                return View(fileEntityDTO);
            }
        }


    }
}