using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PrintersManager.Models;
using PrintersManager.Services;

namespace PrintersManager.Controllers;

[Route("api/installations")]
public class InstallationsController: Controller
{
    private readonly IInstallationsService _installationsService;

    public InstallationsController(IInstallationsService service)
    {
        _installationsService = service;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetInstallationsAsync([FromQuery] int? branchId = null)
    {
        var installations = await _installationsService.GetInstallationsAsync(branchId);
        if (!installations.Any()) return NotFound("No installations were found"); 
        return Ok(installations);
    }
    
    [HttpGet("by-id")]
    public async Task<IActionResult> GetInstallationByExternalIdAsync([FromQuery] string externalId)
    {
        var isValid = Guid.TryParse(externalId, out var guidId);
        if (!isValid) return BadRequest("Id should be a valid guid");
        
        var installation = await _installationsService.GetInstallationByExternalIdAsync(guidId);
        if (installation is null) return NotFound("No installations with provided id were found"); 
        return Ok(installation);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewInstallation([FromBody] AddInstallationRequest request, CancellationToken token)
    {
        AddInstallationResponse response = await _installationsService.AddNewInstallationAsync(request, token);

        if (response.InstallationId is not null)
        {
            return Created(new Uri(Request.GetEncodedUrl()+ "/" + request.Name), response.InstallationId);
        }

        return BadRequest(response.ErrorMessage);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteInstallation([FromQuery] string externalId)
    {
        var isValid = Guid.TryParse(externalId, out var guidId);
        if (!isValid) return BadRequest("Id should be a valid guid");
        
        var installation = await _installationsService.DeleteInstallationAsync(guidId);
        if (installation is false) return NotFound("No installations with provided id were found"); 
        return Ok(installation);
    }
}