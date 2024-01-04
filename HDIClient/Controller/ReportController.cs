using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;
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
            var model = new NewReportViewModel();
            try
            {
                (listPolicy, var code) = await _service.GetPolicyByDriver(token, idUser);
                if (code == HttpStatusCode.OK)
                {
                    model.policyList = listPolicy;
                }
                else if (code == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("LoginView", "Account");
                }
                else
                {
                    return RedirectToAction("ErrorNotFound", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorServer", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> ViewReport(string id)
        {
            var model = new ReportViewModel();
            var token = User.FindFirst("token").Value;
            try
            {


                var (report, code) = await _reportService.GetReportById(token, id);
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
                    return RedirectToAction("ErrorNotFound", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorServer", "Home");
            }
        }
        //solo los ajustadores
        [HttpPost("UpdateOpinion")]
        public async Task<IActionResult> UpdateOpinion([FromBody] NewOpinionadjusterDTO reportViewModel)
        {
            // Lógica para editar la opinión del ajustador
            var description = reportViewModel.Description;
            var idOpinion = reportViewModel.IdOpinionAdjuster;
            var token = User.FindFirst("token").Value;
            
            try
            {
                var opinion = new NewOpinionadjusterDTO();
                opinion.Description = description;
                opinion.IdOpinionAdjuster = idOpinion;
                opinion.CreationDate = DateTime.Now;

                var code = await _reportService.PutOpionion(opinion, token);

                if (code == HttpStatusCode.OK)
                {

                    return Ok();

                }
                else if (code == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(new { ErrorMessage = "Su sesión ha expirado." });
                }
                else
                {
                    return NotFound(new { ErrorMessage = "Ha ocurrido un problema al procesar su solicitud" });
                }
            }
            catch (Exception)
            {
                // Log the exception
                return BadRequest(new { ErrorMessage = "Ha ocurrido un error con nuestro servicio, inténtelo más tarde." });
            }
        }
        //solo los ajustadores
        [HttpPost("CreatOpinion")]
        public async Task<IActionResult> CreatOpinion([FromBody] NewOpinionadjusterDTO reportViewModel)
        {
            // Lógica para crear la opinión del ajustador
            var token = User.FindFirst("token").Value;
            var description = reportViewModel.Description;
            var idReport = reportViewModel.IdAccident;
             
            // Lógica para crear la opinión del ajustador
            try
            {
                var opinion = new NewOpinionadjusterDTO();
                opinion.Description = description;
                opinion.IdAccident = idReport;
                opinion.CreationDate = DateTime.Now;

                var code = await _reportService.PostOpionion(opinion, token);

                if (code == HttpStatusCode.OK)
                {
                    return Ok();

                }
                else if (code == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(new { ErrorMessage = "Su sesión ha expirado." });
                }
                else
                {
                    return NotFound(new { ErrorMessage = "Ha ocurrido un problema al procesar su solicitud" });
                }
            }
            catch (Exception)
            {
                // Log the exception
                return BadRequest(new { ErrorMessage = "Ha ocurrido un error con nuestro servicio, inténtelo más tarde." });
            }


        }
        //solo los conductores
        [HttpPost("InfoReport")]
        public async Task<IActionResult> InfoReport([FromBody] ReportData reportData)
        {
            var token = User.FindFirst("token").Value;
            var idUser = User.FindFirst(ClaimTypes.Sid).Value;
            var lat = reportData.Latitude;
            var lon = reportData.Longitude;
            var add = reportData.Address;
            var id = reportData.Idcar;
            var listimg = reportData.ImageByteList;
            var listinv = reportData.InvolvedDataList;


            var listInvolved = new List<InvolvedDTO>();
            var newreport = new NewReport();
            newreport.Location = add;
            newreport.AccidentDate = DateTime.Now;
            newreport.Latitude = lat;
            newreport.Longitude = lon;
            newreport.IdDriverClient = idUser;
            newreport.IdVehicleClient = id;
            newreport.Images = listimg;
            newreport.ReportStatus = "Nuevo";
            foreach (var item in listinv)
            {
                var newinv = new InvolvedDTO();
                var carinv = new CarinvolvedDTO();
                newinv.NameInvolved = item.InvolvedName;
                newinv.LastNameInvolved = item.InvolvedLastName;
                newinv.LicenseNumber = item.InvolvedNumber;
                if (!string.IsNullOrWhiteSpace(item.Plate) || !string.IsNullOrWhiteSpace(item.Model) || !string.IsNullOrWhiteSpace(item.Brand) || !string.IsNullOrWhiteSpace(item.Color))
                {
                    carinv.Model = item.Model;
                    carinv.Color = item.Color;
                    carinv.Plate = item.Plate;
                    carinv.Brand = item.Brand;
                    newinv.CarInvolved = carinv;
                }

                listInvolved.Add(newinv);

            }
            newreport.Involveds = listInvolved;
            try
            {
                var result = await _reportService.PostReport(newreport, token);
                if (result == HttpStatusCode.OK)
                {
                    return Ok(new { Message = "Datos recibidos y procesados exitosamente" });
                }
                else if (result == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(new { ErrorMessage = "Su sesión ha expirado." });
                }
                else
                {
                    return NotFound(new { ErrorMessage = "Ha ocurrido un problema al procesar su solicitud" });
                }
            }
            catch (Exception)
            {
                // Log the exception
                return BadRequest(new { ErrorMessage = "Ha ocurrido un error con nuestro servicio, inténtelo más tarde." });
            }
        }
    }
}
