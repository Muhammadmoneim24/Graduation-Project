
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
    public class AssignmentConfigurations : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a =>a.Id);
            builder.Property(a =>a.Id).ValueGeneratedOnAdd();

            builder.Property(a =>a.Tittle).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(a => a.Describtion).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);
            builder.Property(a => a.EndDate).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);


            builder.HasOne(a =>a.Course).WithMany(a => a.Assignments).HasForeignKey(a => a.CourseId).IsRequired();

            builder.ToTable("Assignments");

        }
    }
}
