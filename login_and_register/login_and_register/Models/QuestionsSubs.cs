namespace login_and_register.Models
{
    public class QuestionsSubs
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string StudentAnswer { get; set; }

        public int SubmissionId { get; set; }
        public int AnswerPoints { get; set; }

        public virtual Question Question { get; set; }
        public virtual Submission Submission { get; set; }
    }
}
