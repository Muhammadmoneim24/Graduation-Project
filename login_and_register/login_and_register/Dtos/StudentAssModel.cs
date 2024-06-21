using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Dtos
{
    public class StudentAssModel
    {
        public int AssignmentId { get; set; }

        public string UserEmail { get; set; }

        public int Grade { get; set; }

    }
}
