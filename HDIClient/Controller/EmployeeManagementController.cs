using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HDIClient.Controllers
{
    public class EmployeeManagementController : Controller
    {
        private IEmployeeService _employeeService;
        public EmployeeManagementController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

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

        public IActionResult EditInfoEmployee(string id)
        {
            ViewData["IdUserEdit"] = id;
            // Redirige directamente a la acción de edición en lugar de crear una nueva instancia del controlador
            return RedirectToAction("EditRegisterEmployeeView", "EditEmployee", new {id});
            ////////////////////
            //var Roles = new Dictionary<string, string>
            //{
            //    {"ADMIN","Administrador"},
            //    {"ADJUSTER","Ajustador"},
            //    {"OTHER","El de servicio"},
            //};
            //var selectList = new SelectList(Roles, "Key", "Value");
            //var DTOObject = _employeeService.GetEmployeeById(TempData["IdUserEdit"] as string);
            ////iniciamos el modelo a enviar a la vista
            //var model = new EmployeeViewModel
            //{
            //    NameEmployeee = DTOObject.Result.Item2.NameEmployee,
            //    LastnameEmployee = DTOObject.Result.Item2.LastnameEmployee,
            //    Username = DTOObject.Result.Item2.Username,
            //    Password = DTOObject.Result.Item2.Password,
            //    ListaDeRoles = selectList,
            //    Rol = DTOObject.Result.Item2.Rol
            //};
            //return View("EditEmployeeView", model);
            //////////////////////

        }
        public IActionResult RegisterNewEmployee()
        {
            
            return RedirectToAction("GetRegisterEmployeeView", "registerEmployee");
        }


    }
}
