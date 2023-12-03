using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
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
                policyList = listPolicy,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectVehicle(string vehicleId, List<PolicyDTO> list)
        {
            // Lógica para seleccionar el vehículo y actualizar el ViewModel
            var select = new VehicleclientDTO();
            foreach (var vehicle in list)
            {
                if (vehicle.IdVehicleClient == vehicleId)
                {

                    select = vehicle.VehicleClient;
                }

            }
            var model = new NewReportViewModel
            {
                VehicleSelected = select,
            };

            // Regresa el partial view junto con el ViewModel
            return PartialView("_PartialNewReport", model);
        }

        public IActionResult ViewReport()
        {
            //recuperar el reporte
           return View();
        }
    }
}
