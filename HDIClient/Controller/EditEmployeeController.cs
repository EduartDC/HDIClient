﻿using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service;
using HDIClient.Service.Interface;
using HDIClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HDIClient.Controllers
{
    [Authorize]
    public class EditEmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeViewModel viewmodelTemp;
        //ESAT ES UNICAMENTE PARA PRUEBA
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
            // idE = ViewData["IdUserEdit"] as string;
            var token = User.FindFirst("token").Value;
            var DTOObject = _employeeService.GetEmployeeById(id, token);
            //iniciamos el modelo a enviar a la vista
            var model = new EmployeeViewModel
            {

                NameEmployeee = DTOObject.Result.Item2.NameEmployee,
                LastnameEmployee = DTOObject.Result.Item2.LastnameEmployee,
                Username = DTOObject.Result.Item2.Username,
                Password = Encryption.Decrypt(DTOObject.Result.Item2.Password),
                ListaDeRoles = selectList,
                Rol = DTOObject.Result.Item2.Rol
            };
            TempData["OriginalEmployeeModel"] = JsonConvert.SerializeObject(model);
            return View("EditEmployeeView", model);

        }

        [Authorize(Roles = "admin")]
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
                    Password = Encryption.Encrypt(newEmployee.Password),
                    Rol = newEmployee.Rol
                };
                var token = User.FindFirst("token").Value;
                var result = await _employeeService.SetUpdateEmployee(employeeTemp, token);

                if (result == 200 || result == 201)
                {
                    TempData["Prueba"] = true;
                    // var view = GetEmployeeManagementView();
                    //return view;
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
            var originalModelJson = TempData["OriginalEmployeeModel"] as string;
            var originalModel = JsonConvert.DeserializeObject<EmployeeViewModel>(originalModelJson);
            var Roles = new Dictionary<string, string>
            {
                {"admin","admin"},
                {"ajustador","ajustador"},
                {"asistente","asistente"},
            };
            var selectList = new SelectList(Roles, "Key", "Value");
            originalModel.ListaDeRoles = selectList;
            return View("EditEmployeeView",originalModel);

        }


    }
}