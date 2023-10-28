using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index(string searching)
        { 
            return View();
        }


        public async Task<IActionResult> GetItems(string path)
        {

            
        }
        

       

       
    }
}