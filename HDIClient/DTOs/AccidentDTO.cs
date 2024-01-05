using System.ComponentModel.DataAnnotations;

namespace HDIClient.DTOs
{
    public class AccidentDTO
    {
        public string idAccident { get; set; }
        [Display(Name = "Ubicación")]
        public string location { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string nameLocation { get; set; }
        [Display(Name = "Estado del Reporte")]
        public string reportStatus { get; set; }
        [Display(Name = "Fecha del Siniestro")]
        public DateTime accidentDate { get; set; }
        public string idDriverClient { get; set; }
        public string idEmployee { get; set; }
        public string idOpinionAdjuster { get; set; }
        public string idVehicleClient { get; set; }
    }
}
