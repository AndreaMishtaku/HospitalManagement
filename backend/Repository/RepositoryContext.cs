using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class RepositoryContext:IdentityDbContext<User,ApplicationRole,int>
 {
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Client>? Client{ get; set; }
    public DbSet<Equipment>? Equipment { get; set; }
    public DbSet<Menu>? Menu { get; set; }
    public DbSet<Reservation>? Reservation { get; set; }
    public DbSet<Services>? Services{ get; set; }
    public DbSet<ServiceEquipment>? ServiceEquipment { get; set; }
    public DbSet<ServiceStaff>? ServiceStaff { get; set; }
    public DbSet<WorkingHours>? WorkingHours { get; set; }
    public DbSet<CheckIn>? CheckIn { get; set; }   
}

