using Microsoft.AspNetCore.Mvc;

namespace HDIClient.Controllers
{
    public class RegisterEmployeeController : Controller
    {
        public RegisterEmployeeController() { }

        public IActionResult RegisterEmployee()
        {
            return View("RegisterEmployee");
        }
    }
}
