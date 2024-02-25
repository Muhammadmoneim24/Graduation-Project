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
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Content).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();

            builder.HasOne(c =>c.ApplicationUser).WithMany(c => c.Comments).HasForeignKey(c => c.ApplicationUserId).IsRequired();
            builder.HasOne(c => c.Discussion).WithMany(c => c.Comments).HasForeignKey(c => c.DiscussionId).IsRequired();



            builder.ToTable("Comments");

        }
    }
}
