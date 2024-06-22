using login_and_register.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace login_and_register.Configurations
{
    public class UserFriendConfigurations : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            builder.HasKey(uf => new { uf.UserId, uf.FriendId });

           builder.HasOne(uf => uf.User).WithMany(u => u.Friends).HasForeignKey(uf => uf.UserId).OnDelete(DeleteBehavior.Restrict);

           builder.HasOne(uf => uf.Friend).WithMany(u => u.FriendOf).HasForeignKey(uf => uf.FriendId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
