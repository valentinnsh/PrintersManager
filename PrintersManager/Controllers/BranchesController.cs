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
    public async Task<IActionResult> GetAllBranchesAsync()
    {
        var branches = await _branchesService.GetBranchesAsync();
        return Ok(branches);
    }
}