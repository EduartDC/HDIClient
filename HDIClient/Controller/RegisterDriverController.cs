using AseguradoraApp.Models;
using HDIClient.Service.Interface;
using HDIClient.Utility;
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
             
                if(ContieneMayusculaYNumero(newDriver.Password))
                {
                    if(ContieneSoloNumeros(newDriver.TelephoneNumber))
                    {
                        newDriver.Password = Encryption.Encrypt(newDriver.Password);
                        var result = await _clientservice.RegisterNewClientDriver(newDriver);
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
                            return RedirectToAction("ErrorServer", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "telefono invalido");
                        TempData["TelefonoInvalido"] = true;
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("Error", "Contrasena invalida");
                    TempData["ContrasenaInvalida"] = true;
                }

 

               
            }

            return View("RegisterDriver");

        }

        public static bool ContieneMayusculaYNumero(string input)
        {
            bool contieneMayuscula = input.Any(char.IsUpper);    
            bool contieneNumero = input.Any(char.IsDigit);
            return contieneMayuscula && contieneNumero;
        }

        public static bool ContieneSoloNumeros(string input)
        {
            // Verificar si todos los caracteres son numéricos
            return input.Length == 10 && input.All(char.IsDigit);
        }

    }
}
