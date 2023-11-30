using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HDIClient.Models
{
    public class NewReportViewModel
    {
        [Display(Name = "Localización (Búsqueda en Google Maps):")]
        public string? Localizador { get; set; }

        public int Latitud { get; set; }

        public int Longitud { get; set; }

        [Display(Name = "Medio de transporte:")]
        public SelectList? Transportes { get; set; }
        public string? Transporte { get; set; }

        [Display(Name = "Distancia entre tu ubicación y el destino:")]
        public string? Distancia { get; set; }

        [Display(Name = "Tiempo entre tu ubicación y el destino:")]
        public string? Tiempo { get; set; }

        [Display(Name = "Mostrar la ruta")]
        public bool Ruta { get; set; }
    }
}
