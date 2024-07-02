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
    public async Task<IActionResult> GetAllOrders()
    {
        // TODO: Handle exceptions through middleware
        try
        {
            var employees = await _employeesService.GetEmployeesAsync();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex} An error occurred while retrieving printers.");
        }
    }
}