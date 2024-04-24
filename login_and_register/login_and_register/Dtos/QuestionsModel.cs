using login_and_register.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class QuestionsModel
    {
        public string ?type { get; set; }

        public string? question { get; set; }
        public string []? choices { get; set; }

        public string? correctAnswer { get; set; }
        public int points { get; set; }

        public string? explanaition { get; set; }
        public string []? selectedAnswers { get; set; }



    }
}
