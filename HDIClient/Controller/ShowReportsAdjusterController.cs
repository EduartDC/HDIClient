using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HDIClient.Controllers
{
    public class ShowReportsAdjusterController : Controller
    {
        private IReportService _reportService;
        private EmployeeViewModel viewmodelTemp;
        private string idUserPrueba = "1d";//ESAT ES UNICAMENTE PARA PRUEBA
        public ShowReportsAdjusterController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> ShowReportsAjusterView()
        {
            string idAjustadorPrueba = "e2";
            //lista para uno de los filtros
            var StatusList = new Dictionary<string, string>
            {
                {"Concluido","Concluido"},
                {"Activo","Activo"},
            };
            var selectList = new SelectList(StatusList, "Key", "Value");
            var result = _reportService.GetPreviewReportsList(idAjustadorPrueba);

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

            }




            return View("ShowReportsAdjuster");

        }

    }
}
