using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petlover.Models;

namespace Petlover.Context;

public class PetloverDbContext:IdentityDbContext<User>
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public PetloverDbContext(DbContextOptions opt):base(opt)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(PetloverDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
