using HDIClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Tips()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewReport()
        {
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
                Ruta = true
            };
            return View(model);
        }
    }
}
