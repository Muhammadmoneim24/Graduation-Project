namespace login_and_register.Dtos
{
    public class MessageModel
    {

        public string senderUsername { get; set; }

        public string receiverUsername { get; set; }
        public string message { get; set; }
        public int FileId { get; set; }
            
    }
}
