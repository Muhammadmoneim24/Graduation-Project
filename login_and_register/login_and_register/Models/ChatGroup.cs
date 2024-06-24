using System.Collections.Generic;

namespace login_and_register.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<ChatGroupMember> Members { get; set; } 
        public ICollection<ChatMessage> Messages { get; set; } 
    }
}