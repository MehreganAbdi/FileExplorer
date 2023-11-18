using FileExplorer.Data.structs;
using FileExplorer.DTOs;
using FileExplorer.IService;
using FileExplorer.Services;
using FileExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Controllers
{
    public class FileEntityController : Controller
    {
        private readonly IFileEntityService fileEntityService;
        private readonly IProjectService projectService;
        private readonly IDirectoryService directoryService;

        public FileEntityController(IFileEntityService fileEntityService,
                                    IProjectService projectService,
                                    IDirectoryService directoryService)
        {
            this.fileEntityService = fileEntityService;
            this.projectService = projectService;
            this.directoryService = directoryService;
        }

        public async Task<IActionResult> Index()
        {
            var allfileEntities = await fileEntityService.GetAllAsync();
            return View(allfileEntities);
        }

       

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allProjects = new ProjectsStruct(){AllProjects = await projectService.GetAllAsync()}; 
            ViewBag.data = allProjects;



            ViewBag.recentPaths = fileEntityService.LastFivePaths();


            var reloadSafety = new FileEntityDTO();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FileEntityDTO fileEntityDTO)
        {
             var allProjects = await projectService.GetAllAsync();
            ViewBag.data = allProjects;

            ViewBag.recentPaths = fileEntityService.LastFivePaths();

            try
            {

                if (!await directoryService.ValidatePathPattern(fileEntityDTO.FilePath))
                {
                    fileEntityDTO.NamingErrorTD = "Path Is Not Valid";
                    return View(fileEntityDTO);
                }

                if (fileEntityDTO.FileToCopy == null)
                {
                    fileEntityDTO.Error = "A File Must Be Selected";
                    return View(fileEntityDTO);
                }


                fileEntityDTO.ProjectName = (await projectService.GetByIdAsync(fileEntityDTO.ProjectId)).ProjectName;



                var file = fileEntityDTO.FileToCopy;
                var finalFileEntityDTO = await fileEntityService.CreateFileEntityDTODirectly(file, fileEntityDTO);

                var result = await fileEntityService.AddFileEntityAsync(finalFileEntityDTO);
                if (!result)
                {
                    fileEntityDTO.CreateErrorTD = "An Error Happened While Creating , try Again";
                    return View(finalFileEntityDTO);
                }

                fileEntityDTO.CreateErrorTD = "File Added Successfully";

                return RedirectToAction("Index", "FileEntity");
            }
            catch (Exception ex)
            {
                ViewBag.data = allProjects;

                fileEntityDTO.CreateErrorTD = ex.Message.ToString();

                return View(fileEntityDTO);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                ViewBag.data = await projectService.GetAllAsync();

                var fileEntity = await fileEntityService.GetByIdAsync(Id);

                if (fileEntity == null)
                {
                    TempData["EditError"] = "Couldn't Find The File Or File Doesn't Exist";
                    return RedirectToAction("Index", "FileEntity");
                }


                return View(fileEntity);
            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message.ToString();
                return RedirectToAction("Index", "FileEntity");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FileEntityDTO fileEntityDTO)
        {
            ViewBag.data = await projectService.GetAllAsync();
            if (!ModelState.IsValid)
            {
                fileEntityDTO.Error = "Make Sure To All Gaps Are Filled";

                return View(fileEntityDTO);
            }
            try
            {
                fileEntityDTO.ProjectName = (await projectService.GetByIdAsync(fileEntityDTO.ProjectId)).ProjectName;

                var result = await fileEntityService.UpdateAsync(fileEntityDTO);
                if (!result)
                {
                    fileEntityDTO.EditErrorTD = "Update Failed ,Try Again";

                    return View(fileEntityDTO);
                }

                return RedirectToAction("Index", "FileEntity");

            }
            catch (Exception ex)
            {
                fileEntityDTO.EditErrorTD = ex.Message.ToString();
                return View(fileEntityDTO);
            }

        }






        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var fileEntity = await fileEntityService.GetByIdAsNoTrackingAsync(Id);
                if (fileEntity == null)
                {
                    TempData["DeleteError"] = "File Doesn't Exists";

                    return RedirectToAction("Index", "FileEntity");
                }

                var result = await fileEntityService.RemoveFileEntityAsync(fileEntity);
                if (!result)
                {
                    TempData["DeleteError"] = "File Didn't Delete , Try Again";
                    
                    return Ok(false);

                }
                TempData["Error"] = "File Record Deleted Successfully";

                return Ok(true);
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = ex.Message.ToString();
                
                return Ok(false);
            }
        }





    }
}
