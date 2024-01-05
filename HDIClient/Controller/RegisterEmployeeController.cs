using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using HDIClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    [Authorize]
    public class RegisterEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp;
        public RegisterEmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult GetRegisterEmployeeView()
        {
            var Roles = new Dictionary<string, string>
            {
                {"admin","admin"},
                {"ajustador","ajustador"},
                {"asistente","asistente"},
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

        [Authorize(Roles = "admin")]
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
                    Password = Encryption.Encrypt(newEmployee.Password),
                    Rol = newEmployee.Rol
                };

                var result = await _employeeService.RegisterNewEmployee(employeeTemp);

                if (result == 200 || result == 201)
                {
                    TempData["RegistroExitoso"] = true;
                    
                    return View("RegisterEmployeeSuccesView");
                }
                else if (result == 409)
                {
                    ModelState.AddModelError("Error", "username registrada por otro usuario");
                    TempData["ErrorLicenciaExistente"] = true;
                }
                else
                {
                    return RedirectToAction("ErrorServer", "Home");
                }

            }



            return View("RegisterEmployeeView", viewmodelTemp);

        }
    }
}
