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
            builder.HasOne(a => a.ApplicationUser).WithMany(a => a.SubmissionAssignments).HasForeignKey(a => a.ApplicationUserId);
            builder.HasOne(s => s.Assignment).WithMany(s => s.SubmissionAssignments).HasForeignKey(s => s.AssignmentId);


        }
    }
}
