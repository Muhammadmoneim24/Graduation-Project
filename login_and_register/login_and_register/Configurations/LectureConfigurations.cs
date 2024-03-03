using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_and_register.Configurations
{
    public class LectureConfigurations : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x=>x.Name).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.Link).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);
            builder.HasOne(x=>x.Course).WithMany(x=>x.Lectures).HasForeignKey(x=>x.CourseId);

            builder.ToTable("Lectures");

        }
    }
}
