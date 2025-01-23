using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Services.Abstracts;

public interface IDepartmentService
{
    Task CreateDepartment(DepartmentCreateVM vm);
    Task UpdateDepartment(int id, DepartmentUpdateVM vm);
    Task DeleteDepartment(int id);
    Task<DepartmentItemVM> GetDepartmentById(int id);
    Task<ICollection<DepartmentItemVM>> GetDepartments();
}
