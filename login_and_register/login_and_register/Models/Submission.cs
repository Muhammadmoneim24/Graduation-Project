using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Submission
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public int Grade { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public string? StudentAnswer {  get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Exam Exam { get; set; }

        public Question Question { get; set; }
    }
}
