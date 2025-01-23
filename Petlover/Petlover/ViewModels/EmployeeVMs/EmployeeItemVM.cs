using Petlover.Models;

namespace Petlover.ViewModels.EmployeeVMs;

public class EmployeeItemVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ImageUrl { get; set; }
    public Department Department { get; set; }
}
