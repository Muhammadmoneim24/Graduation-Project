using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_and_register.Configurations
{
    public class ChatGroupMemberConfigurations : IEntityTypeConfiguration<ChatGroupMember>
    {
        public void Configure(EntityTypeBuilder<ChatGroupMember> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.HasOne(m => m.User).WithMany(cm => cm.GroupMembers).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Group).WithMany(g => g.Members).HasForeignKey(m => m.GroupId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(m => m.IsAdmin).IsRequired();

            builder.ToTable("ChatGroupMembers");
        }
    }
}