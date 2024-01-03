using HDIClient.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HDIClient.Models
{
    public class ReportViewModel
    {
        [Display(Name = "Localización (Búsqueda en Google Maps):")]
        public string? Localizador { get; set; }

        public int? Latitud { get; set; }

        public int? Longitud { get; set; }

        public ReportDTO? Report { get; set; }

        public string? Description { get; set; }
        public string? IdOpinionAdjuster { get; set; }
        public string? IdReport { get; set; }
    }
}
