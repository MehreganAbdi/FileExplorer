using FileExplorer.IService;
using FileExplorer.Models;
using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;

        public HomeController(IDirectoryService directoryService)
        {
            this.directoryService = directoryService;
        }
        public async Task<IActionResult> Index(string path ,string searching)
        {
            if (path == null)
            {
                return View(new FileExploreViewModel());
            }


            var pathData = await directoryService.GetDataInViewModel(path);


            if (pathData == null)
            {
                return NotFound();
            }


            if (searching != null)
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

            }

            return View(pathData);
        }

        
    }
}