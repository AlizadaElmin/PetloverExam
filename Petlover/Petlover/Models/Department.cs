namespace Petlover.Models;

public class Department:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
