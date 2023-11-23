using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HDIClient.Models
{
    public class User
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nombre")]
        public string NameEmployeee {  get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Apellido")]
        public string LastnameEmployee { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nmbre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Fecha de registro")]
        public DateTime RegistrationDate {get; set;} 
    }
}
