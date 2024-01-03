using AseguradoraApp.Models;
using HDIClient.Service.Interface;
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

        public async Task<IActionResult> RegisterNewDriver([Bind("NameDriver,LastNameDriver,TelephoneNumber,LicenseNumber,Password")] DriverClient newDriver)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientservice.RegisterNewClientDriver(newDriver);
                var x = result;

                if (result == 200 || result == 201)
                {
                    TempData["RegistroExitoso"] = true;
                    return RedirectToAction("Login", "Login");
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
