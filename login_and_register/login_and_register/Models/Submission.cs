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

        public string ApplicationUserId { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Exam Exam { get; set; }

        public virtual ICollection<QuestionsSubs> QuestionsSubs { get; set; }
    }
}
