
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
    public class NotificationConfigrations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n =>n.Id).ValueGeneratedOnAdd();   

            builder.Property(n =>n.Subject).HasColumnType("VARCHAR").HasMaxLength(256).IsRequired(false);



            builder.ToTable("Notifications");
        }
    }
}
