using AseguradoraApp.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AseguradoraApp.Controllers
{

    public class RegisterDriverController : Controller
    {
        IClientService _clientservice;


        public RegisterDriverController(IClientService clientService)
        {
            _clientservice = clientService;
        }

        public IActionResult RegisterDriverDos()
        {
            return View("RegisterDriver");
        }

        public async Task<IActionResult> RegisterNewDriver([Bind("NameDriver,LastNameDriver,AgeString,TelephoneNumber,LicenseNumber,Password")] DriverClient newDriver)
        {
            if (ModelState.IsValid)
            {
                newDriver.Age = DateOnly.ParseExact(newDriver.AgeString, "yyyy-MM-dd");
                // Formatear la fecha al nuevo formato
                string formattedDate = newDriver.Age.ToString("dd/MM/yyyy");
                newDriver.Age = DateOnly.Parse(formattedDate);
                var result = await _clientservice.RegisterNewClientDriver(newDriver);
                var x = result;

                if (result == 200 || result == 201)
                {
                    TempData["RegistroExitoso"] = true;
                    // return RedirectToAction("LoginView", "Account");
                    return View("RegisterDriverSuccesView");
                }
                else if (result == 409)
                {
                    ModelState.AddModelError("Error", "Licencia registrada por otro usuario");
                    TempData["ErrorLicenciaExistente"] = true;
                }
                else
                {
                    ModelState.AddModelError("Error", "Error de conexion");
                    TempData["ErrorConexion"] = true;
                }
            }

            return View("RegisterDriver");

        }


    }
}
