using System.ComponentModel.DataAnnotations;

namespace AseguradoraApp.Models
{
    public class DriverClient
    {
        public string IdDriverClient { get; set; } = "";

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nombre")]
        public string NameDriver { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Apellido")]
        public string LastNameDriver { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
        public string AgeString { get; set; }
        public DateOnly Age { get; set; }
        public string FullNameDriver { get; set; } = "";

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Numero telefonico")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo Teléfono solo puede contener números.")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Numero de licencia de conducir")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}

//using System.ComponentModel.DataAnnotations;

//namespace AseguradoraApp.Models
//{
//    public class DriverClient
//    {
//        [Required(ErrorMessage = "El campo es obligatorio")]
//        [Display(Name = "Nombre")]
//        public string DriverName { get; set; }

//        [Required(ErrorMessage = "El campo es obligatorio")]
//        [Display(Name = "Apellido")]
//        public string DriverLastname { get; set; }

//        //[Required(ErrorMessage = "El campo es obligatorio")]
//        //[Display(Name = "Nacimiento")]
//        //public DateTime DriverBirthday { get; set; }

//        [Required(ErrorMessage = "El campo es obligatorio")]
//        [Display(Name = "Numero telefonico")]
//        [RegularExpression(@"^\d+$", ErrorMessage = "El campo Teléfono solo puede contener números.")]
//        public string TelephoneNumber { get; set; }

//        [Required(ErrorMessage = "El campo es obligatorio")]
//        [Display(Name = "Numero de licencia de conducir")]
//        public string License { get; set; }

//        [Required(ErrorMessage = "El campo es obligatorio")]
//        [Display(Name = "Contraseña")]
//        public string DriverPassword { get; set; }
//    }
//}
