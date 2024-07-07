
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
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c =>c.Id).ValueGeneratedOnAdd();


            builder.Property(c =>c.CourseName).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(c => c.Description).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired();
            builder.Property(c => c.Playlist).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);



            builder.ToTable("Courses");

        }
    }
}
