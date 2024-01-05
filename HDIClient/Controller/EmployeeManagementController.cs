using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDIClient.Controllers
{
    [Authorize]
    public class EmployeeManagementController : Controller
    {
        private IEmployeeService _employeeService;
        public EmployeeManagementController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EmployeeManagementView()
        {
            var token = User.FindFirst("token").Value;
            var result = _employeeService.GetEmployeeList(token);
            if (result.Result.Item1 == 200)
            {
                var model = new EmployeeListViewModel
                {
                    employeeList = result.Result.Item2
                };
                return View("EmployeeManagementView", model);
            }
            else
            {
                return RedirectToAction("ErrorServer", "Home");
            }

        }

        [Authorize(Roles = "admin")]
        public IActionResult EditInfoEmployee(string id)
        {
            ViewData["IdUserEdit"] = id;
            // Redirige directamente a la acción de edición en lugar de crear una nueva instancia del controlador
            return RedirectToAction("EditRegisterEmployeeView", "EditEmployee", new {id});
        }
        public IActionResult RegisterNewEmployee()
        {
            return RedirectToAction("GetRegisterEmployeeView", "registerEmployee");
        }


    }
}
