using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Services.Abstracts;

public interface IEmployeeService
{
    Task CreateEmployee(EmployeeCreateVM vm, string destination);
    Task UpdateEmployee(int id,EmployeeUpdateVM vm, string? destination=null);
    Task DeleteEmployee(int id);
    Task<EmployeeItemVM> GetEmployeeById(int id);
    Task<ICollection<EmployeeItemVM>> GetEmployees();

}
