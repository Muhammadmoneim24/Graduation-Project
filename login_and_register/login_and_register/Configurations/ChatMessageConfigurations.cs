using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_and_register.Configurations
{
    public class ChatMessageConfigurations : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            
            builder.HasKey(cm => cm.Id);
            
            builder.Property(cm => cm.Message).IsRequired().HasMaxLength(5000); 

            builder.Property(cm => cm.Timestamp).IsRequired();
            
            builder.HasOne(cm => cm.Sender).WithMany(user => user.SentMessages).HasForeignKey(cm => cm.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(cm => cm.Receiver).WithMany(user => user.ReceivedMessages).HasForeignKey(cm => cm.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            
            builder.ToTable("ChatMessages");
        }
    }
}
