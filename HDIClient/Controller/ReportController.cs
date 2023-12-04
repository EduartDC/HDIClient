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
        readonly IReportService _reportService;

        public ReportController(IPolicyService service, IReportService reportService)
        {
            _service = service;
            _reportService = reportService;

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

            var model = new NewReportViewModel
            {
                policyList = listPolicy,
            };
            return View(model);
        }

        public IActionResult Camara()
        {
            // Lógica para tomar fotos con la cámara
            return PartialView("_PartialCamara");
        }

        public IActionResult Galeria()
        {
            // Lógica para subir fotos desde la galería
            return PartialView("_PartialFiles");
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

        public async Task<IActionResult> ViewReport()
        {
            var token = User.FindFirst("token").Value;
            var report  = await _reportService.GetReportById(token, "ACC3NOBORRAR");
            //recuperar el reporte
            var model = new ReportViewModel
            {
                Report = report,
            };
           return View(model);
        }
    }
}
