using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Models
{
    public class SubmissionAssignment
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        
        public string ApplicationUserId { get; set; }

        public byte[]? File { get; set; }

        public int Grade { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Assignment Assignment { get; set; }
    }
}
