using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class EnrollmentModel
    {
        //public string StudentId { get; set; }
        //public int CourseId { get; set; }

        public string Email { get; set; }

        public int CourseId { get; set; }



    }
}
