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
    public class ExamConfigurations : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Tittle).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);
            builder.Property(e => e.Describtion).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(e => e.Instructions).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(e => e.Time).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Grades).HasColumnType("INT").IsRequired();
            builder.Property(e => e.NumOfQuestions).HasColumnType("INT").IsRequired();
            builder.Property(e => e.Date).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);


            builder.HasOne(x=>x.Course).WithMany(x=>x.Exams).HasForeignKey(x => x.CourseId);
            builder.HasMany(x => x.Questions).WithOne(e => e.Exam).HasForeignKey(e => e.ExamId); 


            builder.ToTable("Exams");
        }
    }
}
