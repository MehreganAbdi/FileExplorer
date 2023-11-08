using FileExplorer.DTOs;
using FileExplorer.IService;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Controllers
{
    public class FileEntityController : Controller
    {
        private readonly IFileEntityService fileEntityService;
        private readonly IProjectService projectService;

        public FileEntityController(IFileEntityService fileEntityService,
                                    IProjectService projectService)
        {
            this.fileEntityService = fileEntityService;
            this.projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var allfileEntities = await fileEntityService.GetAllAsync();
            return View(allfileEntities);
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

                return RedirectToAction("Index", "Project");
            }
            catch (Exception ex)
            {
                TempData["CreateError"] = ex.Message.ToString();
                return View(fileEntityDTO);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var fileEntity = await fileEntityService.GetByIdAsNoTrackingAsync(Id);

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
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Make Sure To All Gaps Are Filled";

                return View(fileEntityDTO);
            }
            try
            {
                var result = await fileEntityService.UpdateAsync(fileEntityDTO);
                if (!result)
                {
                    TempData["EditError"] = "Update Failed ,Try Again";

                    return View(fileEntityDTO);
                }

                TempData["EditError"] = "FilEntity Updated Successfully";

                return RedirectToAction("Index", "FileEntity");

            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message.ToString();
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
                    return RedirectToAction("Index", "FileEntity");

                }
                TempData["Error"] = "File Record Deleted Successfully";

                return RedirectToAction("Index", "fileEntity");
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = ex.Message.ToString();
                return RedirectToAction("Index", "FileEntity");
            }
        }

    }
}
