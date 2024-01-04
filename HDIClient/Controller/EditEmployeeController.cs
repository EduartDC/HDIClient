using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class EditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp;
       //ESAT ES UNICAMENTE PARA PRUEBA
        public EditEmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

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
           // idE = ViewData["IdUserEdit"] as string;
            var DTOObject = _employeeService.GetEmployeeById(id);
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
            //  TempData["IdUserEdit"] = DTOObject.Result.Item2.IdEmployee;



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
                    IdEmployee = TempData["IdUserEdit"] as string,
                    NameEmployee = newEmployee.NameEmployeee,
                    LastnameEmployee = newEmployee.LastnameEmployee,
                    Username = newEmployee.Username,
                    Password = newEmployee.Password,
                    Rol = newEmployee.Rol
                };

                var result = await _employeeService.SetUpdateEmployee(employeeTemp);

                if (result == 200 || result == 201)
                {
                    TempData["Prueba"] = true;
                    var view = GetEmployeeManagementView();
                    return view;
                }
                else if (result == 409)
                {
                    ModelState.AddModelError("Error", "username registrada por otro usuario");
                    TempData["ErrorLicenciaExistente"] = true;
                }

            }



            return View("LoginView");

        }

        private IActionResult GetEmployeeManagementView()
        {
            // Crear una instancia del controlador EmployeeManagementController y pasarle el mismo servicio
            var employeeManagementController = new EmployeeManagementController(_employeeService);

            // Llamar al método EmployeeManagementView del controlador
            var result = employeeManagementController.EmployeeManagementView().Result;

            // Devolver el resultado
            return result;
        }
    }
}
