using System.ComponentModel.DataAnnotations;

namespace HDIClient.DTOs
{
    public class EmployeeDTO
    {
        public string? IdEmployee { get; set; } = "0";
        [Display(Name = "Nombre")]
        public string NameEmployee { get; set; }
        [Display(Name = " del Ajustador")]
        public string LastnameEmployee { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}
