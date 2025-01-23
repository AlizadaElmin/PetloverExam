using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petlover.Extensions;
using Petlover.Services.Abstracts;
using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class EmployeeController(IEmployeeService _employeeService,IDepartmentService _departmentService,IMapper _mapper,IWebHostEnvironment _env):Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _employeeService.GetEmployees());
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _departmentService.GetDepartments();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateVM vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            return View();
        }
        if (vm.Image != null)
        {
            if (!vm.Image.IsValidSize(300))
                ModelState.AddModelError("Size", "Image is not valid size");
            if (!vm.Image.IsValidType("image"))
                ModelState.AddModelError("Type", "Image is not valid type");

            ViewBag.Departments = await _departmentService.GetDepartments();
        }
        string destination = _env.WebRootPath;
        await _employeeService.CreateEmployee(vm,destination);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null) throw new Exception("Employee not found");
        EmployeeUpdateVM employeeVM = _mapper.Map<EmployeeUpdateVM>(employee);
        ViewBag.Departments = await _departmentService.GetDepartments();
        return View(employeeVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, EmployeeUpdateVM vm)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null) throw new Exception("Employee not found");
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            return View();
        }
        string? destination = null;
        if (vm.Image != null)
        {
            if (!vm.Image.IsValidSize(30))
                ModelState.AddModelError("Size", "Image is not valid size");
            if (!vm.Image.IsValidType("image"))
                ModelState.AddModelError("Type", "Image is not valid type");
            
            destination = _env.WebRootPath;
            
        }
        await _employeeService.UpdateEmployee(id, vm,destination);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.DeleteEmployee(id);
        return RedirectToAction(nameof(Index));
    }
}
