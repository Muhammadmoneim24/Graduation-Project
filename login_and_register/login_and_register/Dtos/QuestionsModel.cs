using System.ComponentModel.DataAnnotations;

namespace login_and_register.Dtos
{
    public class QuestionsModel
    { 
        public string ?Text { get; set; }
        public string? Options { get; set; }

        public string? CorrectAnswer { get; set; }
        public string? Explanation { get; set; }

    }
}
