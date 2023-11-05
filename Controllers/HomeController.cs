using FileExplorer.DTOs;
using FileExplorer.IService;

using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Runtime.InteropServices;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;
        private readonly IEmailService emailService;

        public HomeController(IDirectoryService directoryService , IEmailService emailService)
        {
            this.directoryService = directoryService;
            this.emailService = emailService;
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


         
        public async Task<IActionResult> SaveFileDirectly(string pathhh)
        {
            await directoryService.DownloadFileInDownloads(pathhh);

            return View("Index");
        
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
                await directoryService.AddFileToPath(fileExploreViewModel.path, fileExploreViewModel.SelectedFile);
                var redirectedFileExploreViewModel = await directoryService.GetDataInViewModel(fileExploreViewModel.path);
                redirectedFileExploreViewModel.path = fileExploreViewModel.path;

                TempData["AddResult"] = "File Successfully Added To \n  >" + fileExploreViewModel.path.ToString();
                return View("Index", redirectedFileExploreViewModel);
            }
            catch (Exception ex)
            {
                TempData["AddResult"] = ex.Message.ToString();
                return View("Index", fileExploreViewModel);

            }

        }

        public async Task<IActionResult> EmailListReult(FileExploreViewModel fileExploreViewModel)
        {
            var data = await directoryService.GetDataInViewModel(fileExploreViewModel.path);
            directoryService.CreateLocalFile(directoryService.ConverViewModelTostring(data));

            var emailDTO = new EmailDTO() {
                Reciever = fileExploreViewModel.Reciever,
            Subject = "Your Requested File",
            message ="File : "
            };

            await emailService.SendFileByEmail(emailDTO, "FileData.txt");
            directoryService.DeleteLocalFile();

            return View("Index", fileExploreViewModel);
        } 
    }
}