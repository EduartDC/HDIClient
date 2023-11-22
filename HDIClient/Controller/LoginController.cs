using HDIClient.Models;
using HDIClient.Service;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace HDIClient.Controllers
{
    public class LoginController : Controller
    {
        IUserService _service;
        public LoginController(IUserService service, IMemoryCache memoryCache)
        {
            _service = service;
           
        }
        public IActionResult LoginView()
        {

            return View();
        }

        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                
                var user = loginModel.User;
                var password = loginModel.Password;

                var result = _service.Login(user, password);
                if (await result == "OK")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o contraseña invalido");
                }
            }
            return View("LoginView");
        }
    }
}
