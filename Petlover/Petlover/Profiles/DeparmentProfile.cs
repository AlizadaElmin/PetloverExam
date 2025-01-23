using AutoMapper;
using Petlover.Models;
using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Profiles;

public class DeparmentProfile:Profile
{
    public DeparmentProfile()
    {
        CreateMap<DepartmentCreateVM, Department>();
        CreateMap<DepartmentUpdateVM, Department>().ReverseMap();
        CreateMap<DepartmentUpdateVM, DepartmentItemVM>().ReverseMap();
        CreateMap<Department, DepartmentItemVM>();
    }
}
