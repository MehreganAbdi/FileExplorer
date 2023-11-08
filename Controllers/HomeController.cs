﻿using FileExplorer.DTOs;
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
                    TempData["Error"] = "Insert An Existing Path";
                    return View(new FileExploreViewModel());
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
                        TempData["Error"] = "Nothing Found";
                        return RedirectToAction("Index", "Home");
                    }

                }


                return View(pathData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message.ToString();
                return View("Index");
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
                    TempData["Error"] = "Insert An Existing Path";

                    return RedirectToAction("Index", "Home");
                }
                await directoryService.NewFolder(path, NewFolderName);
                TempData["AddResult"] = NewFolderName == null ? "NewFolder Created Successfuly In \n  >" + path.ToString() :
                                                               $"{NewFolderName} Created Successfuly In \n  >" + path.ToString();
                var redirectedFileExploreViewModel = await directoryService.GetDataInViewModel(path);
                redirectedFileExploreViewModel.path = path;

                return View("Index", redirectedFileExploreViewModel);
            }
            catch (Exception ex)
            {
                TempData["AddResult"] = ex.Message.ToString();
                return View("Index");

            }

        }
        [HttpPost]
        public async Task<IActionResult> AddFilesToPath(FileExploreViewModel fileExploreViewModel)
        {


            try
            {
                if (fileExploreViewModel.SelectedFile == null)
                {
                    TempData["SelectError"] = "Select A File First";

                    return RedirectToAction("Index", fileExploreViewModel);

                }

                var typo = fileExploreViewModel.SelectedFile.ContentType;

                if (typo != "image/jpg" || typo != "image/png" || typo != "image/jpeg" )
                {
                    TempData["SelectError"] = "Selected File Must Be img";
                    return View("Index", fileExploreViewModel);
                }

                await directoryService.AddFileToPath(fileExploreViewModel.path, fileExploreViewModel.SelectedFile);

                var redirectedFileExploreViewModel = await directoryService.GetDataInViewModel(fileExploreViewModel.path);

                redirectedFileExploreViewModel.path = fileExploreViewModel.path;

                TempData["AddResult"] = "File Successfully Added To \n  >" + fileExploreViewModel.path.ToString();



                var fileEntityDTO = new FileEntityDTO()
                {
                    ProjectId = 13,
                    ProjectName = "NotDefined",
                    Type = fileExploreViewModel.path.Split(".")[^2],
                    DateCreated = DateTime.Now,
                    Size = new FileInfo(fileExploreViewModel.path).Length.ToString() + "B",
                    Description = "NotDefined",
                    FilePath = fileExploreViewModel.path,
                    Name = fileExploreViewModel.path.Split(".")[^1]
                };

                
                var result = await fileEntityService.AddFileEntityAsync(fileEntityDTO);


                if (!result)
                {
                    TempData["CreateError"] = "Couldn't Copy the File , Try Again";
                }


                return View("Create", fileEntityDTO);


            }
            catch (Exception ex)
            {
                TempData["AddResult"] = ex.Message.ToString();
                return View("Create", fileExploreViewModel);

            }
        }




        public async Task<IActionResult> EmailListResult(FileExploreViewModel fileExploreViewModel)
        {
            if (fileExploreViewModel.Reciever == null)
            {
                TempData["Error"] = "To Send You Results Through Email , You Need To Fill Email Address Field";

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
            var newFileExploreViewModel = await directoryService.GetDataInViewModel(fileExploreViewModel.path);

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
                TempData["Error"] = ex.Message.ToString();
                var fileExploreViewModel = await directoryService.GetDataInViewModel(bothpath.Split("&&&")[^1]);
                fileExploreViewModel.path = bothpath.Split("&&&")[^1];

                return View("Index", fileExploreViewModel);
            }
        }

        public async Task<IActionResult> DownloadView(string path, string type, string directory)
        {
            var fileViewModel = await directoryService.GetDataInViewModel(directory);
            fileViewModel.path = directory;

            if (Path.Exists(path))
            {
                await Download(path, type);

                return View("Index", fileViewModel);
            }
            else
            {
                TempData["DownloadError"] = "File Has Been Deleted , Refresh To Get Latest Update";
                return View("Index", fileViewModel);
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


        public async Task<IActionResult> AddToRecords(string path, string size, string type)
        {

            var fileEntityDTO = new FileEntityDTO()
            {
                Name = path.Split(".")[^2],
                Size = size,
                DateCreated = DateTime.Now,
                Description = "NotDefined",
                FilePath = path,
                ProjectName = "NotDefined",
                ProjectId = 13,
                Type = path.Split(".")[^1]
            };

            await fileEntityService.AddFileEntityAsync(fileEntityDTO);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allProjects = await projectService.GetAllAsync();
            ViewBag.data = allProjects;



            var reloadSafety = new FileEntityDTO();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FileEntityDTO fileEntityDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["CreateError"] = "Make Sure That You Filled All Gaps";

                    return View(fileEntityDTO);
                }
                var projectByName = await projectService.GetProjectByNameAsync(fileEntityDTO.ProjectName);
                fileEntityDTO.ProjectId = projectByName.Id;


                var result = await fileEntityService.AddFileEntityAsync(fileEntityDTO);
                if (!result)
                {
                    TempData["CreateError"] = "An Error Happened While Creating , try Again";
                    return View(fileEntityDTO);
                }

                TempData["CreateError"] = "File Added Successfully";

                return RedirectToAction("Index", "FileEntity");
            }
            catch (Exception ex)
            {
                var allProjects = await projectService.GetAllAsync();
                ViewBag.data = allProjects;

                TempData["CreateError"] = ex.Message.ToString();

                return View(fileEntityDTO);
            }
        }


    }
}