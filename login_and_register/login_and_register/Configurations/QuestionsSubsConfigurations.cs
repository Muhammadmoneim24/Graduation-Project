using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_and_register.Configurations
{
    public class QuestionsSubsConfigurations : IEntityTypeConfiguration<QuestionsSubs>
    {
        public void Configure(EntityTypeBuilder<QuestionsSubs> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.StudentAnswer).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);

            builder.HasOne(e=>e.Submission).WithMany(e=>e.QuestionsSubs).HasForeignKey(e=>e.SubmissionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Question).WithMany(e => e.QuestionsSubs).HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);



            builder.HasIndex(s => s.SubmissionId).IsUnique(false);
            builder.HasIndex(s => s.QuestionId).IsUnique(false);



            builder.ToTable("QuestionsSubs");
        }
    }
}
