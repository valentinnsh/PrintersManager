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
    public async Task<IActionResult> GetAllPrinters([FromQuery] int? connectionType = null)
    {
        var printers = await _printersService.GetPrintersAsync(connectionType is null
            ? null : (ConnectionTypes)connectionType);
        return Ok(printers);
    }
}