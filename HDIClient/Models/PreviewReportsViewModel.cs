using HDIClient.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HDIClient.Models
{
    public class PreviewReportsListViewModel
    {
        public List<PreviewReportDTO> previewList { get; set; }

        [Display(Name = "Localización (Búsqueda en Google Maps):")]
        public string? Localizador { get; set; }

        public double Latitud { get; set; } = 19.541142;

        public double Longitud { get; set; } = -96.9271873;

    }
}
