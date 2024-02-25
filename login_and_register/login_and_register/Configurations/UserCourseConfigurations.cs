
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
    internal class UserCourseConfigurations : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Course).WithMany(x=>x.UserCourses).HasForeignKey(x => x.CourseId).IsRequired();
            builder.HasOne(x=>x.ApplicationUser).WithMany(x=>x.UserCourses).HasForeignKey(x=>x.ApplicationUserId).IsRequired();

            builder.ToTable("UserCourses");
        }
    }
}
