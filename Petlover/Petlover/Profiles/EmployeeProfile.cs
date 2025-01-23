using AutoMapper;
using Petlover.Models;
using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Profiles;

public class EmployeeProfile:Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeCreateVM, Employee>();
        CreateMap<EmployeeUpdateVM, Employee>();
        CreateMap<EmployeeUpdateVM, EmployeeItemVM>().ReverseMap();
        CreateMap<Employee, EmployeeItemVM>();
    }
}
