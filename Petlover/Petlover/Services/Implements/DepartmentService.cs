using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Petlover.Context;
using Petlover.Models;
using Petlover.Services.Abstracts;
using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Services.Implements;

public class DepartmentService(PetloverDbContext _context, IMapper _mapper) : IDepartmentService
{
    public async Task CreateDepartment(DepartmentCreateVM vm)
    {
        Department department = _mapper.Map<Department>(vm);
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartment(int id)
    {
        Department? department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        if (department == null) throw new Exception("Department not found");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<DepartmentItemVM> GetDepartmentById(int id)
    {
        Department? department = await _context.Departments.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
        if (department == null)
            throw new Exception("Department not found");
        DepartmentItemVM departmentItem = _mapper.Map<DepartmentItemVM>(department);
        return departmentItem;
    }

    public async Task<ICollection<DepartmentItemVM>> GetDepartments()
    {
        ICollection<Department> departments = await _context.Departments.Include(x => x.Employees).ToListAsync();
        if (departments == null)
            throw new Exception("Departments not found");
        ICollection<DepartmentItemVM> departmentItems = _mapper.Map<ICollection<DepartmentItemVM>>(departments);
        return departmentItems;
    }

    public async Task UpdateDepartment(int id, DepartmentUpdateVM vm)
    {
        Department? department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        if (department == null)
            throw new Exception("Department not found");
        _mapper.Map(vm, department);
        await _context.SaveChangesAsync();
    }
}
