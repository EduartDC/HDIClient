﻿using HDIClient.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HDIClient.Models
{
    public class ReportViewModel
    {
        [Display(Name = "Localización (Búsqueda en Google Maps):")]
        public string? Localizador { get; set; }

        public int? Latitud { get; set; }

        public int? Longitud { get; set; }

        public ReportDTO? Report { get; set; }
    }
}
