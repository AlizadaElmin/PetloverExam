namespace Petlover.Models;

public class Employee:BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ImageUrl { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
}
