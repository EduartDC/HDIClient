using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class RegisterEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp;
        public RegisterEmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult GetRegisterEmployeeView()
        {
            var Roles = new Dictionary<string, string>
            {
                {"ADMIN","Administrador"},
                {"ADJUSTER","Ajustador"},
                {"OTHER","El de servicio"},
            };
            var selectList = new SelectList(Roles, "Key", "Value");
            //iniciamos el modelo a enviar a la vista
            var model = new EmployeeViewModel
            {
                ListaDeRoles = selectList,
            };
            // viewmodelTemp = model;
            return View("RegisterEmployeeView", model);
        }

        public async Task<IActionResult> RegisterNewEmployee([Bind("NameEmployeee,LastnameEmployee,Username,Password,Rol,ListaDeRoles")] EmployeeViewModel newEmployee)
        {
            var x = newEmployee;
            if (ModelState.IsValid)
            {
                //generando DTO
                EmployeeDTO employeeTemp = new EmployeeDTO()
                {
                    NameEmployee = newEmployee.NameEmployeee,
                    LastnameEmployee = newEmployee.LastnameEmployee,
                    Username = newEmployee.Username,
                    Password = newEmployee.Password,
                    Rol = newEmployee.Rol
                };

                var result = await _employeeService.RegisterNewEmployee(employeeTemp);

                if (result == 200 || result == 201)
                {
                    TempData["RegistroExitoso"] = true;
                    return RedirectToAction("Login", "Login");
                }
                else if (result == 409)
                {
                    ModelState.AddModelError("Error", "username registrada por otro usuario");
                    TempData["ErrorLicenciaExistente"] = true;
                }

            }



            return View("RegisterEmployeeView", viewmodelTemp);

        }
    }
}
