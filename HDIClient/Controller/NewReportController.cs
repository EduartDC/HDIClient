using HDIClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HDIClient.Controllers
{
    //[ServiceFilter(typeof(AuthorizationFilter))]
    public class NewReportController : Controller
    {
        public IActionResult NewReportView()
        {
            return View();
        }
    }
}
