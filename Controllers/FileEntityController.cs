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


        [HttpGet]
        public async Task<IActionResult> CreateFromHome(string path, string name, string type, string size)
        {
            var fileEntityDTO = new FileEntityDTO()
            {
                Size = size,
                DateCreated = DateTime.Now,
                Description = "Not Defined Yet",
                FilePath = path,
                Name = name,
                ProjectName = "NotDefined",
                Type = type,
                ProjectId = 13
            };

            ViewBag.data = await projectService.GetAllAsync();
            return View(fileEntityDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFromeHome(FileEntityDTO fileEntityDTO)
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

                return RedirectToAction("Index", "FileEntity");
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = ex.Message.ToString();
                return RedirectToAction("Index", "FileEntity");
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
                var typo = fileExploreViewModel.path;
                if (typo != "jpg" || typo!="png" || typo!="jpeg"|| typo != "gif")
                {
                    TempData["SelectError"] = "Selected File Must Be img";
                    return View("Index",fileExploreViewModel);
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

                return View("CreateFromHome", result);
            }
            catch (Exception ex)
            {
                TempData["AddResult"] = ex.Message.ToString();
                return View("Index", fileExploreViewModel);

            }
        }



    }
}
