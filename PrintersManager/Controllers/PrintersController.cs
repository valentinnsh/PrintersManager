using Database;
using Microsoft.AspNetCore.Mvc;
using PrintersManager.Models;
using PrintersManager.Services;

namespace PrintersManager.Controllers;

[Route("api/printers")]
public class PrintersController: Controller
{
    private readonly IPrintersService _printersService;

    public PrintersController(IPrintersService service)
    {
        _printersService = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] int? connectionType = null)
    {
        // TODO: Handle exceptions through middleware
        try
        {
            var printers = await _printersService.GetPrintersAsync(connectionType is null
                ? null : (ConnectionTypes)connectionType);
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex} An error occurred while retrieving printers.");
        }
    }
}