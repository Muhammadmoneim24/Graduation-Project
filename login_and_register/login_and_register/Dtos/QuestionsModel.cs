using login_and_register.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class QuestionsModel
    {
        public string type { get; set; }

        public string question { get; set; }
        public string []? choices { get; set; } = null;

        public string? correctAnswer { get; set; }     = string.Empty;
        public int points { get; set; }

        public string? explanaition { get; set; }                     = string.Empty;
        public string []? selectedAnswers { get; set; }



    }
}
