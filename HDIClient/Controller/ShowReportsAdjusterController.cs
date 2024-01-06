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

        [Authorize(Roles = "ajustador")]
        public async Task<IActionResult> ShowReportsAjusterView()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Sid);
            string userId = userIdClaim.Value;
            var token = User.FindFirst("token").Value;
            var result =  _reportService.GetPreviewReportsList(userId,token);
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

        [Authorize(Roles = "ajustador")]
        public IActionResult ShowFullReport(string id)
        {
            return RedirectToAction("ViewReport", "Report", new { id });
        }
    }
}
