using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HDIClient.DTOs
{
    public class LoginDTO
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
