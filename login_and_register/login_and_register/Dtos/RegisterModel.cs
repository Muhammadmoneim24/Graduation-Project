using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace login_and_register.Dtos
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, EmailAddress ,StringLength(128)]
        public string Email { get; set; }

        [Required, PasswordPropertyText,StringLength(256, MinimumLength = 6)]
        public string Password { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; }

    }
}
