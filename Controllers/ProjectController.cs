
using FileExplorer.Data.structs;
using FileExplorer.DTOs;
using FileExplorer.IService;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var allProjects = await projectService.GetAllAsync(); 

            return View(allProjects);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var reloadSaftey = new ProjectDTO();
            return View(reloadSaftey);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDTO projectDTO)
        {
            try
            {
                
                if (await projectService.ProjectExists(projectDTO.ProjectName))
                {
                    projectDTO.CreateErrorTD =  "There Is An Existing Project With This Name";
                    return View(projectDTO);
                }

                var addResult = await projectService.AddProjectAsync(projectDTO);
                
                if (!addResult)
                {
                    projectDTO.CreateErrorTD = "Adding Failed , Try Again";
                    return View(projectDTO);
                }

                TempData["Error"] = "Project Created Successfully.";

                return View(projectDTO);
                //return Json(true);
            }
            catch (Exception ex)
            {
                projectDTO.CreateErrorTD = ex.Message.ToString();
                return View(projectDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var project = await projectService.GetByIdAsync(Id);

                if (project == null)
                {
                    TempData["EditError"] = "Item With This Id Didn't Exist";

                    return RedirectToAction("Index", "Project");
                }
                
                return View(project);

            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message.ToString();

                return RedirectToAction("Index", "Project");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectDTO projectDTO)
        {
            try
            {
                if (projectDTO == null)
                {
                    projectDTO.EditErrorTD = "Inser Value To Edit";
                    return View(projectDTO);
                }

                 await projectService.UpdateAsync(projectDTO);
                

                return RedirectToAction("Index", "Project");

            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message.ToString();
                return View(projectDTO);
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var project = await projectService.GetByIdAsNoTrackingAsync(Id);
                if (project == null)
                {
                    TempData["DeleteError"] = "Project Didn't Exist";
                    return RedirectToAction("Index", "Project");

                }
                project.Id = Id;
                await projectService.RemoveProjectAsync(project);
                
                TempData["DeleteError"] = " File Deleted Successfully";

                return Ok(true);
            }
            catch(Exception ex)
            {
                TempData["DeleteError"] = ex.Message.ToString();

                return Ok(false);

            }
        }





    }
}
