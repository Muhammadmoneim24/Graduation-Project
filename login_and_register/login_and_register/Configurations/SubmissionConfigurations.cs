
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
    public class SubmissionConfigurations : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s =>s.Id).ValueGeneratedOnAdd();


            builder.Property(s => s.Grade).HasColumnType("INT");
            builder.Property(s => s.StudentAnswer).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);

            builder.HasOne(s => s.ApplicationUser).WithOne(s => s.Submission).HasForeignKey<Submission>(s =>s.ApplicationUserId);
            builder.HasOne(s => s.Exam).WithOne(s => s.Submission).HasForeignKey<Submission>(s => s.ExamId);
            builder.HasOne(s => s.Question).WithOne(s => s.Submission).HasForeignKey<Submission>(s=>s.QuestionId);


            builder.ToTable("Submissions");

        }
    }
}
