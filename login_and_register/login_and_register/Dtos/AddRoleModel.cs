using System.ComponentModel.DataAnnotations;

namespace login_and_register.Dtos
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
