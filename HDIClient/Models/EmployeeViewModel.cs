using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HDIClient.Models
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nombre")]
        public string NameEmployeee { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Apellido")]
        public string LastnameEmployee { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nmbre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        public string? Rol { get; set; }

        //   public DateTime? RegistrationDate { get; set; }

        [Display(Name = "Rol")]
        public SelectList? ListaDeRoles { get; set; }

    }
}
