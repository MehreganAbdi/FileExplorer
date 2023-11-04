using FileExplorer.IService;
 
using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;
 

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;

        public HomeController(IDirectoryService directoryService)
        {
            this.directoryService = directoryService;
        }
        public async Task<IActionResult> Index(string path, string searching)
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

        public async Task<IActionResult> NewFolder(string path, string? NewFolderName="NewFolder")
        {
            if (path == null || !directoryService.PathExists(path))
            {
                TempData["Error"] = "Insert An Existing Path";

                return RedirectToAction("Index", "Home");
            }

            await directoryService.NewFolder(path, NewFolderName);
            TempData["AddResult"] =NewFolderName == null ?"NewFolder Created Successfuly In \n  >"+path.ToString():
                                                           $"{NewFolderName} Created Successfuly In \n  >" + path.ToString();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> AddFilesToPath(FileExploreViewModel fileExploreViewModel)
        {
            if(fileExploreViewModel.SelectedFile == null)
            {
                TempData["SelectError"] = "Select A File First";
                return RedirectToAction("Index", "Home");
            }
            await directoryService.AddFileToPath(fileExploreViewModel.path, fileExploreViewModel.SelectedFile);
            TempData["AddResult"] = "File Successfully Added To \n  >" + fileExploreViewModel.path.ToString();
            return RedirectToAction("Index", "Home");
        }
    }
}