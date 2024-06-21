using login_and_register.Models;
using System.Text.Json.Serialization;

namespace login_and_register.Dtos
{
    public class ExamModel
    {
        public string? Tittle { get; set; }

        public string? Describtion { get; set; }

        public string? Instructions { get; set; }

        public string? Time { get; set; }

        public int Grades { get; set; }
        public int NumOfQuestions { get; set; }
        public string? EndDate { get; set; }

       

    }
}
