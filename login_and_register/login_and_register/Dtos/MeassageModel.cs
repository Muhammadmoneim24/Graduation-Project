namespace login_and_register.Dtos
{
    public class MeassageModel
    {
        public string receiverUserId { get; set; }
        public string message { get; set; }
        public IFormFile? file { get; set; }
            
    }
}
