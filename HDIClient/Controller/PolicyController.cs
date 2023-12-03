using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
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
            try
            {
                listPolicy = await _service.GetPolicyByDriver(token, idUser);
            }
            catch (Exception)
            {
                //
            }
         
            var model = new PolicyViewModel
            {
                PolicyList = listPolicy,
                
            };
            return View(model);
        }
    }
}
