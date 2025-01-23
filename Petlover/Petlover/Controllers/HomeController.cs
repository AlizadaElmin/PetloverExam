using Microsoft.AspNetCore.Mvc;
using Petlover.Models;
using Petlover.Services.Abstracts;
using System.Diagnostics;

namespace Petlover.Controllers;

public class HomeController(IEmployeeService _service) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _service.GetEmployees());
    }
}
