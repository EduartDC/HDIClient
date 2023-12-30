using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
            var model = new ReportViewModel();
            var token = User.FindFirst("token").Value;
            var (report, code)  = await _reportService.GetReportById(token, "a1");
            if (code == HttpStatusCode.OK)
            {
                model.Report = report;
                return View(model);
            }
            else if (code == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("LoginView", "Account");
            }
            else
            {
                return RedirectToAction("ServerError", "Home");
            }
           
        }

        public async Task<IActionResult> UpdateOpinion(ReportViewModel reportViewModel)
        {
            // Lógica para editar la opinión del ajustador
            var description = reportViewModel.Description;
            var idOpinion = reportViewModel.IdOpinionAdjuster;
            
            var allowedCharsRegex = @"[\w\s.,?!0-9]";
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(idOpinion))
            {
                // Lógica para mostrar mensaje de error
            }
            else if (!Regex.IsMatch(description, $"^{allowedCharsRegex}*$"))
            {
                // Lógica para mostrar mensaje de error
            }
            else
            {
                try
                {
                    
                    var opinion = new NewOpinionadjusterDTO();
                    opinion.Description = description;
                    opinion.IdOpinionAdjuster = idOpinion;
                    opinion.CreationDate = DateTime.Now;

                    var code = await _reportService.PutOpionion(opinion);

                    if (code == HttpStatusCode.OK)
                    {
                        // Lógica para mostrar mensaje de éxito
                    }
                    else if (code == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("LoginView", "Account");
                    }
                    else
                    {
                        // Lógica para mostrar mensaje de error
                    }
                }
                catch (Exception)
                {
                    // Lógica para mostrar mensaje de error de conexión
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreatOpinion(ReportViewModel reportViewModel)
        {
            // Lógica para crear la opinión del ajustador
            var description = reportViewModel.Description;
            var idReport = reportViewModel.IdReport;
            var allowedCharsRegex = @"[\w\s.,?!0-9]";
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(idReport))
            {
                // Lógica para mostrar mensaje de error
            }
            else  if(!Regex.IsMatch(description, $"^{allowedCharsRegex}*$"))
            {
                // Lógica para mostrar mensaje de error
            }
            else
            {
                // Lógica para crear la opinión del ajustador
                try
                {
                    var opinion = new NewOpinionadjusterDTO();
                    opinion.Description = description;
                    opinion.IdAccident = idReport;
                    opinion.CreationDate = DateTime.Now;

                    var code = await _reportService.PostOpionion(opinion);

                    if (code == HttpStatusCode.OK)
                    {
                        // Lógica para mostrar mensaje de éxito
                    }
                    else if (code == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("LoginView", "Account");
                    }
                    else
                    {
                        // Lógica para mostrar mensaje de error
                    }
                }
                catch (Exception)
                {
                    // Lógica para mostrar mensaje de error de conexión
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
