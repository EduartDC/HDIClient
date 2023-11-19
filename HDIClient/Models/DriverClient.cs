using System.ComponentModel.DataAnnotations;

namespace AseguradoraApp.Models
{
    public class DriverClient
    {
        [Required(ErrorMessage ="El campo es obligatorio")]
        [Display(Name = "")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "")]
        public string DriverLastname { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "")]
        public string License { get; set; }
    }
}
