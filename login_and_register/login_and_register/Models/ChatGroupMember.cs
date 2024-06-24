namespace login_and_register.Models
{
    public class ChatGroupMember
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsAdmin { get; set; }
        public ApplicationUser User { get; set; }
        public ChatGroup Group { get; set; }
    }
}