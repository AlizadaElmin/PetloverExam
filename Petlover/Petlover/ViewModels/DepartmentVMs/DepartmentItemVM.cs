using Petlover.Models;

namespace Petlover.ViewModels.DepartmentVMs;

public class DepartmentItemVM
{
    public  int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
