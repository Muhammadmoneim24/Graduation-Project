using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class SubmissionModel
    {
        public string Email { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }
        public List<QuestionSub> Questionssub { get; set; }


    }

    public class QuestionSub 
    {
       public int QuestionId { get; set; }

        public string StudentAnswer { get; set; } 
    }
}
