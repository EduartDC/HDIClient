using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace HDIClient.Controllers
{
    public class PolicyController : Controller
    {
        readonly IPolicyService _service;

        public PolicyController(IPolicyService service, IMemoryCache memoryCache)
        {
            _service = service;

        }
        public async Task<IActionResult> ViewPolicy()
        {
            var token = User.FindFirst("token").Value;
            var idUser = User.FindFirst(ClaimTypes.Sid).Value;
            var listPolicy = new List<PolicyDTO>();
            var model = new PolicyViewModel();
            try
            {
                (listPolicy, var code) = await _service.GetPolicyByDriver(token, idUser);
                if (code == HttpStatusCode.InternalServerError)
                {
                    //mensaje de error de servidor
                }
                else if(code == HttpStatusCode.Unauthorized)
                {
                    //mensaje de error de autorización
                }
                else if (code == HttpStatusCode.OK)
                {
                    model.PolicyList = listPolicy;
                }
                else
                {
                    //mensaje de error
                }
            }
            catch (Exception)
            {
                //error de conexión
            }

            return View(model);
        }
    }
}
