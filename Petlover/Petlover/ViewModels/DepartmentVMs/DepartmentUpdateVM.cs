using System.ComponentModel.DataAnnotations;

namespace Petlover.ViewModels.DepartmentVMs;

public class DepartmentUpdateVM
{
    [Required, MaxLength(64)]
    public string Name { get; set; }
}
