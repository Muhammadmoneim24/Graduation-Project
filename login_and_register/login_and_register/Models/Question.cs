using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Question
    {
        public int Id { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public string Type { get; set; }
        public string Text { get; set; }
        public string? Options { get; set; } = string.Empty;
        public string? CorrectAnswer { get; set; }  = string.Empty;
        public int Points { get; set; }
        public string? Explanation { get; set; } = string.Empty;

        public virtual Exam Exam { get; set; } 

        public virtual ICollection<QuestionsSubs> QuestionsSubs { get; set; }
    }
}
