
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
    public class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q =>q.Id).ValueGeneratedOnAdd();

            builder.Property(q => q.Type).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            builder.Property(q =>q.Text).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired();
            builder.Property(q => q.Options).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);
            builder.Property(q => q.CorrectAnswer).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);
            builder.Property(q => q.Explanation).HasColumnType("VARCHAR").HasMaxLength(1000).IsRequired(false);
            builder.Property(q => q.Points).HasColumnType("INT").IsRequired();    
    
            builder.ToTable("Questions");

        }
    }
}
