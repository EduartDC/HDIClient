using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class ShowReportsAdjusterController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp ;
        private string idUserPrueba = "1d";//ESAT ES UNICAMENTE PARA PRUEBA
        public ShowReportsAdjusterController(IEmployeeService employeeService) { 
        _employeeService = employeeService;
        }

        public async Task<IActionResult> ShowReportsAjusterView()
        {
            var StatusList = new Dictionary<string, string>
            {
                {"Concluido","Concluido"},
                {"Activo","Activo"},
            };
            var selectList = new SelectList(StatusList, "Key", "Value");
            var DTOReports =   _employeeService.GetEmployeeById(TempData["IdUserEdit"] as string);
            //iniciamos el modelo a enviar a la vista
            var model = new EmployeeViewModel
            {
                //NameEmployeee =  DTOObject.Result.Item2.NameEmployee,
                //LastnameEmployee = DTOObject.Result.Item2.LastnameEmployee,
                //Username = DTOObject.Result.Item2.Username,
                //Password = DTOObject.Result.Item2.Password,
                //ListaDeRoles = selectList,
                //Rol = DTOObject.Result.Item2.Rol
            };
     



            // viewmodelTemp = model;
            return View("EditEmployeeView", model);
            //return View("LoginView");
        }

    }
}
