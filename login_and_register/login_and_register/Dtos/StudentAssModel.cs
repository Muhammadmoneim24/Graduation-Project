using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Dtos
{
    public class StudentAssModel
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public int Grade { get; set; }

    }
}
