namespace login_and_register.Dtos
{
    public class DiscussionModel
    {
        public string? UserEmail { get; set; }
        public string? Tittle { get; set; }
        public string? Content { get; set; }

        public IFormFile? File { get; set; }
    }
}
