using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDIClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ErrorNotFound()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ErrorServer()
        {
            return View();
        }


    }
}
