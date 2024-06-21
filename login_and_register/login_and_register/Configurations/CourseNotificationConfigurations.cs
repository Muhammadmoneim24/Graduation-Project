
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
    public class CourseNotificationConfigurations : IEntityTypeConfiguration<CourseNotification>
    {
        public void Configure(EntityTypeBuilder<CourseNotification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(x=>x.Content).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x=>x.CourseNotifications).HasForeignKey(x=>x.CourseId).IsRequired();
            builder.HasOne(x => x.Notification).WithMany(x => x.CourseNotifications).HasForeignKey(x => x.NotificationId).IsRequired();

        }
    }
}
