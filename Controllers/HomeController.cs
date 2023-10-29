using FileExplorer.IService;
using FileExplorer.Models;
using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

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
            if (path == null)
            {
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

            return File(System.Text.Encoding.UTF8.GetBytes(dataString), "text/xml", "FileData.txt");
        }
    }
}