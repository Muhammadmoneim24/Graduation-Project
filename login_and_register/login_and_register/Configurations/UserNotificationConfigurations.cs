
using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Configurations
{
    public class UserNotificationConfigurations : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x =>x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Content).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired();

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.UserNotifications).HasForeignKey(x => x.ApplicationUserId).IsRequired();
            builder.HasOne(x => x.Notification).WithMany(x => x.UserNotifications).HasForeignKey(x => x.NotificationId).IsRequired();

            builder.ToTable("UserNotifications");
        }
    }
}
