using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service;
using HDIClient.Service.Interface;
using HDIClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    [Authorize]
    public class EditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp;
        public EditEmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditRegisterEmployeeView(string id)
        {
            TempData["IdUserEdit"] = id;
            var Roles = new Dictionary<string, string>
            {
                {"admin","admin"},
                {"ajustador","ajustador"},
                {"asistente","asistente"},
            };
            var selectList = new SelectList(Roles, "Key", "Value");
            var token = User.FindFirst("token").Value;
            var DTOObject = _employeeService.GetEmployeeById(id,token);
            var model = new EmployeeViewModel
            {
                NameEmployeee = DTOObject.Result.Item2.NameEmployee,
                LastnameEmployee = DTOObject.Result.Item2.LastnameEmployee,
                Username = DTOObject.Result.Item2.Username,
                Password = Encryption.Decrypt(DTOObject.Result.Item2.Password),
                ListaDeRoles = selectList,
                Rol = DTOObject.Result.Item2.Rol
            };
            return View("EditEmployeeView", model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SetUpdateEmployee([Bind("NameEmployeee,LastnameEmployee,Username,Password,Rol,ListaDeRoles")] EmployeeViewModel newEmployee)
        {
            if (ModelState.IsValid)
            {
                EmployeeDTO employeeTemp = new EmployeeDTO()
                {
                    IdEmployee = TempData["IdUserEdit"] as string,
                    NameEmployee = newEmployee.NameEmployeee,
                    LastnameEmployee = newEmployee.LastnameEmployee,
                    Username = newEmployee.Username,
                    Password = Encryption.Encrypt(newEmployee.Password),
                    Rol = newEmployee.Rol
                };
                var token = User.FindFirst("token").Value;
                var result = await _employeeService.SetUpdateEmployee(employeeTemp,token);

                if (result == 200 || result == 201)
                {
                    TempData["Prueba"] = true;
                    return View("EditEmployeeSuccessView");
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



            return View("LoginView");

        }

       
    }
}
