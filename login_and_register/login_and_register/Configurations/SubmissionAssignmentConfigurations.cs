using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_and_register.Configurations
{
    public class SubmissionAssignmentConfigurations : IEntityTypeConfiguration<SubmissionAssignment>
    {
        public void Configure(EntityTypeBuilder<SubmissionAssignment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Grade).HasColumnType("INT");
          

            builder.HasOne(a => a.ApplicationUser)
                   .WithMany(a => a.SubmissionAssignments)
                   .HasForeignKey(a => a.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Assignment)
                   .WithMany(s => s.SubmissionAssignments)
                   .HasForeignKey(s => s.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(sa => sa.AssignmentId).IsUnique(false);
            builder.HasIndex(sa => sa.ApplicationUserId).IsUnique(false);


        }
    }
}
