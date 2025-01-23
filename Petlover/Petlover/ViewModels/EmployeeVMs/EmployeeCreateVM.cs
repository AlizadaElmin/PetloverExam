using Petlover.Models;
using System.ComponentModel.DataAnnotations;

namespace Petlover.ViewModels.EmployeeVMs;

public class EmployeeCreateVM
{
    [Required,MaxLength(32)]
    public string Name { get; set; }
    [Required, MaxLength(32)]
    public string Surname { get; set; }
    public IFormFile Image { get; set; }
    [Required]
    public int DepartmentId { get; set; }
}
