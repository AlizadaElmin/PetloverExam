using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Petlover.Models;

namespace Petlover.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .Property(x => x.Surname)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .HasOne(x=>x.Department)
            .WithMany(d=>d.Employees)
            .HasForeignKey(x=>x.DepartmentId);
    }
}
