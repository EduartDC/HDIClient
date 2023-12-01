using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace HDIClient.Controllers
{
    //[Authorize]
    public class ReportController : Controller
    {
        readonly IPolicyService _service;
        public ReportController(IPolicyService service, IMemoryCache memoryCache)
        {
            _service = service;

        }
        public IActionResult Tips()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewReport()
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
            var Transportes = new Dictionary<string, string>
            {
                {"DRIVING", "Automóvil"},
                {"WALKING", "Caminando"},
                {"BICYCLING", "Bicicleta"}
            };
            var selectList = new SelectList(Transportes, "Key", "Value");

            var model = new NewReportViewModel
            {
                Transportes = selectList,
                policyList = listPolicy,
                Ruta = true
            };
            return View(model);
        }
    }
}
