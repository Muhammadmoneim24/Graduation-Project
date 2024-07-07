using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace login_and_register.Dtos
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
