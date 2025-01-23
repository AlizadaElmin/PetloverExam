using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Petlover.ViewModels.DepartmentVMs;

public class DepartmentCreateVM
{
    [Required,MaxLength(64)]
    public string Name { get; set; }
}
