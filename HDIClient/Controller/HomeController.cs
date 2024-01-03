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

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
