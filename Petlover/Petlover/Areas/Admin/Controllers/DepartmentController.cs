using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petlover.Models;
using Petlover.Services.Abstracts;
using Petlover.ViewModels.DepartmentVMs;
using Petlover.ViewModels.EmployeeVMs;

namespace Petlover.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class DepartmentController(IDepartmentService _service,IMapper _mapper):Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _service.GetDepartments());
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.CreateDepartment(vm);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        var department = await _service.GetDepartmentById(id);
        if (department == null) throw new Exception("Department not found");
        DepartmentUpdateVM departmentVM = _mapper.Map<DepartmentUpdateVM>(department);
        return View(departmentVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id,DepartmentUpdateVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.UpdateDepartment(id,vm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteDepartment(id);
        return RedirectToAction(nameof(Index));
    }
}
