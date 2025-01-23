using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Petlover.Context;
using Petlover.Extensions;
using Petlover.Models;
using Petlover.Services.Abstracts;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Services.Implements;

public class EmployeeService(PetloverDbContext _context,IMapper _mapper) : IEmployeeService
{
    public async Task CreateEmployee(EmployeeCreateVM vm, string destination)
    {
        Employee employee = _mapper.Map<Employee>(vm);
        employee.ImageUrl = await vm.Image.UploadAsync(destination,"images","employees");
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployee(int id)
    {
        Employee? employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null) throw new Exception("Employee not found");

        var path = Path.Combine("wwwroot", "images", "employees", employee.ImageUrl);
        if (File.Exists(path))
            File.Delete(path);

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeItemVM> GetEmployeeById(int id)
    {
        Employee? employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if(employee == null)
            throw new Exception("Employee not found");
       EmployeeItemVM employeeItem = _mapper.Map<EmployeeItemVM>(employee);
        return employeeItem;
    }

    public async Task<ICollection<EmployeeItemVM>> GetEmployees()
    {
        ICollection<Employee> employees = await _context.Employees.Include(x => x.Department).ToListAsync();
        if (employees == null)
            throw new Exception("Employees not found");
        ICollection<EmployeeItemVM> employeeItems = _mapper.Map<ICollection<EmployeeItemVM>>(employees);
        return employeeItems;
    }

    public async Task UpdateEmployee(int id, EmployeeUpdateVM vm, string? destination = null)
    {
        Employee? employee = await _context.Employees.FirstOrDefaultAsync(x=>x.Id == id);
        if (employee == null)
            throw new Exception("Employee not found");
        _mapper.Map(vm,employee);
        if(destination != null)
            employee.ImageUrl = await vm.Image.UploadAsync(destination, "images", "employees");
        await _context.SaveChangesAsync();
    }
}
