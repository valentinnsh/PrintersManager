using Microsoft.AspNetCore.Mvc;
using PrintersManager.Services;

namespace PrintersManager.Controllers;

[Route("api/branches")]
public class BranchesController: Controller
{
    private readonly IBranchesService _branchesService;

    public BranchesController(IBranchesService service)
    {
        _branchesService = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        // TODO: Handle exceptions through middleware
        try
        {
            var branches = await _branchesService.GetBranchesAsync();
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex} An error occurred while retrieving printers.");
        }
    }
}