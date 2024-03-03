namespace login_and_register.Dtos
{
    public class ExamModel
    {
        public string Tittle { get; set; }

        public string Describtion { get; set; }

        public string Instructions { get; set; }

        public string Time { get; set; }

        public int Grades { get; set; }
        public DateTime EndDate { get; set; }
    }
}
