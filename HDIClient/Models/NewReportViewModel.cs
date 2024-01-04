using HDIClient.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HDIClient.Models
{
    public class NewReportViewModel
    {
        [Display(Name = "Localización (Búsqueda en Google Maps):")]
        public string? Localizador { get; set; }

        public int Latitud { get; set; }

        public int Longitud { get; set; }

        public List<PolicyDTO>? policyList { get; set; }

        public string? IdVehicleSelected { get; set; }
        
        public List<InvolvedDTO> InvolvedList { get; set; }

    }
}
