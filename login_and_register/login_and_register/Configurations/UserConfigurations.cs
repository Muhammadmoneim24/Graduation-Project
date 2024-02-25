
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
    public class UserConfigurations:  IEntityTypeConfiguration<ApplicationUser>
    {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //builder.HasKey(s => s.Id);
            //builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.FirstName).HasColumnType("NVARCHAR").HasMaxLength(50).IsRequired();
            builder.Property(s => s.LastName).HasColumnType("NVARCHAR").HasMaxLength(50).IsRequired();
     

        }
    }
}
