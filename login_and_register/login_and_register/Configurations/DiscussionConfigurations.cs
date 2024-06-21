
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
    public class DiscussionConfigurations : IEntityTypeConfiguration<Discussion>
    {
        public void Configure(EntityTypeBuilder<Discussion> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d =>d.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Tittle).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);
            builder.Property(d => d.Content).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);

            builder.HasOne(d => d.ApplicationUser).WithMany(c => c.Discussions).HasForeignKey(d => d.ApplicationUserId).IsRequired(false);
            builder.HasOne(d => d.Course).WithMany(c => c.Discussions).HasForeignKey(d => d.CourseId).IsRequired(false);
            builder.HasMany(d => d.Comments).WithOne(c => c.Discussion).HasForeignKey(d => d.DiscussionId).IsRequired(false);




            builder.ToTable("Discussions");


        }
    }
}
