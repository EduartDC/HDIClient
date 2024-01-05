using HDIClient.Models;
using HDIClient.Service.Interface;
using HDIClient.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Security.Claims;

namespace HDIClient.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly IUserService _service;
        public AccountController(IUserService service, IMemoryCache memoryCache)
        {
            _service = service;

        }
        [AllowAnonymous]
        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {

                var user = loginModel.User;
                var password = Encryption.Encrypt(loginModel.Password);
               

                try
                {
                    var (result, code) = await _service.Login(user, password);
                    if (code == HttpStatusCode.OK)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, result.fullName),
                            new Claim(ClaimTypes.Sid, result.idUser),
                            new Claim(ClaimTypes.Role, result.role),
                            new Claim("token", result.token)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties();
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (code == HttpStatusCode.InternalServerError)
                    {
                        return RedirectToAction("ErrorServer", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario o contraseña invalido");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("ErrorServer", "Home");
                }

            }
            return View("LoginView");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
