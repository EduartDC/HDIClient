using System.ComponentModel.DataAnnotations;

namespace AseguradoraApp.Models
{
    public class DriverClient
    {
        [Required(ErrorMessage ="El campo es obligatorio")]
        [Display(Name = "Nombre")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Apellido")]
        public string DriverLastname { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string DriverBirthday { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Numero telefonico")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Numero de licencia de conducir")]
        public string License { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string DriverPassword { get; set; }
    }
}
