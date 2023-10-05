using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole
            {
                Id = 1,
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new ApplicationRole
            {
                Id = 2,
                Name = "Staff",
                NormalizedName = "STAFF"
            },
            new ApplicationRole
            {
                Id = 3,
                Name = "Reception",
                NormalizedName = "RECEPTION"
            }
        );
    }
}
