using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PrintersManager.Services;

namespace PrintersManager.Controllers;

[Route("api/employees")]
public class EmployeesController: Controller
{
    private readonly IEmployeesService _employeesService;

    public EmployeesController(IEmployeesService service)
    {
        _employeesService = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        var employees = await _employeesService.GetEmployeesAsync();
        return Ok(employees);
    }
}