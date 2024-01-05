using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HDIClient.Controllers
{
    [Authorize]
    public class ShowReportsAdjusterController : Controller
    {
        private IReportService _reportService;
        private EmployeeViewModel viewmodelTemp;
       
        public ShowReportsAdjusterController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ShowReportsAjusterView()
        {
          
            //lista para uno de los filtros
            var StatusList = new Dictionary<string, string>
            {
                {"Concluido","Concluido"},
                {"Activo","Activo"},
            };
            var selectList = new SelectList(StatusList, "Key", "Value");

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);
            string userId = userIdClaim.Value;
            var result = _reportService.GetPreviewReportsList(userId);

            if (result.Result.Item1 == 200)
            {
                var model = new PreviewReportsListViewModel
                {
                    previewList = result.Result.Item2
                };
                return View("ShowReportsAdjuster", model);
            }
            else
            {
                return RedirectToAction("ErrorServer", "Home");
            }
            return View("ShowReportsAdjuster");

        }

        [Authorize(Roles = "admin")]
        public IActionResult ShowFullReport(string id)
        {
            // Redirige directamente a la acción de edición en lugar de crear una nueva instancia del controlador
            return RedirectToAction("ViewReport", "Report", new { id });
           

        }

    }


}
