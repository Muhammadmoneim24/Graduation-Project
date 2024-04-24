using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class CourseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Description { get; set; }
        public IFormFile? photo { get; set; }



    }
}
