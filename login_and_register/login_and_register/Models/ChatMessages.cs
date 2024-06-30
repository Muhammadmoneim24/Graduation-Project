using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_and_register.Models
{
    public class ChatMessage
    {
        
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public int? GroupId { get; set; }
        public string? Message { get; set; }
        public byte[]? File { get; set; }

        public DateTime Timestamp { get; set; }

        public ChatGroup? Group { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}