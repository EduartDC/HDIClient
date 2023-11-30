using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class EditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp ;
        private string idUserPrueba = "1d";//ESAT ES UNICAMENTE PARA PRUEBA
        public EditEmployeeController(IEmployeeService employeeService) { 
        _employeeService = employeeService;
        }

        public async Task<IActionResult> EditRegisterEmployeeView()
        {
            var Roles = new Dictionary<string, string>
            {
                {"ADMIN","Administrador"},
                {"ADJUSTER","Ajustador"},
                {"OTHER","El de servicio"},
            };
            var selectList = new SelectList(Roles, "Key", "Value");
            var DTOObject = _employeeService.GetEmployeeById(idUserPrueba);
            //iniciamos el modelo a enviar a la vista
            var model = new EmployeeViewModel
            {
                NameEmployeee = DTOObject.Result.Item2.NameEmployee,
                LastnameEmployee = DTOObject.Result.Item2.LastnameEmployee,
                Username = DTOObject.Result.Item2.Username,
                Password = DTOObject.Result.Item2.Password,
                ListaDeRoles = selectList,
                Rol = DTOObject.Result.Item2.Rol
            };
            TempData["IdUser"] = DTOObject.Result.Item2.IdEmployee;



            // viewmodelTemp = model;
            return View("EditEmployeeView", model);
            //return View("LoginView");
        }

        public async Task<IActionResult> SetUpdateEmployee([Bind("NameEmployeee,LastnameEmployee,Username,Password,Rol,ListaDeRoles")] EmployeeViewModel newEmployee)
        {
           if (ModelState.IsValid)
           {
                //generando DTO
                EmployeeDTO employeeTemp = new EmployeeDTO()
                {
                    IdEmployee = TempData["IdUser"] as string,
                    NameEmployee = newEmployee.NameEmployeee,
                    LastnameEmployee = newEmployee.LastnameEmployee,
                    Username = newEmployee.Username,
                    Password = newEmployee.Password,
                    Rol = newEmployee.Rol
                };

                var result = await _employeeService.SetUpdateEmployee(employeeTemp);

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
            
            

            return View("LoginView");

        }
    }
}
