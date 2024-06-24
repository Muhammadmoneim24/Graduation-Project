using login_and_register.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Configurations
{
    public class ChatGroupConfigurations : IEntityTypeConfiguration<ChatGroup>
    {
        public void Configure(EntityTypeBuilder<ChatGroup> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd(); 

            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(g => g.Owner).WithMany(u => u.OwnedGroups).HasForeignKey(g => g.OwnerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(g => g.Members).WithOne(m => m.Group).HasForeignKey(m => m.GroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(g => g.Messages).WithOne(m => m.Group).HasForeignKey(m => m.GroupId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ChatGroups");
        }
    }
}
