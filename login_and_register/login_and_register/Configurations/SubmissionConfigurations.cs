
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
            builder.Property(s => s.Id).ValueGeneratedOnAdd();


            builder.Property(s => s.Grade).HasColumnType("INT");


            builder.HasOne(s => s.ApplicationUser).WithMany(s => s.Submissions).HasForeignKey(s => s.ApplicationUserId);
            builder.HasOne(s => s.Exam).WithMany(s => s.Submissions).HasForeignKey(s => s.ExamId).OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(s => s.ExamId).IsUnique(false);



            builder.ToTable("Submissions");

        }
    }
}
