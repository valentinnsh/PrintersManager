using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using PrintersManager.Models;

namespace PrintersManager.Services;

public interface IInstallationsService
{
    Task<GetInstallationResponse?> GetInstallationByExternalIdAsync(Guid externalId, CancellationToken token = default);
    Task<IEnumerable<GetInstallationResponse>> GetInstallationsAsync(int? branchId = null, CancellationToken token = default);
    Task<AddInstallationResponse> AddNewInstallationAsync(AddInstallationRequest request, CancellationToken token = default);
    Task<bool> DeleteInstallationAsync(Guid externalId, CancellationToken token = default);

}

public class InstallationsService : IInstallationsService
{
    private readonly PrintersDbContext _db;

    public InstallationsService(PrintersDbContext context)
    {
        _db = context;
    }
    
    public async Task<GetInstallationResponse?> GetInstallationByExternalIdAsync(Guid externalId, CancellationToken token = default)
    {
        var installationEntity = await _db.Installations
            .Include(i => i.Branch)
            .Include(i => i.Printer)
            .Where(i => i.ExternalId == externalId).FirstOrDefaultAsync(token);

        if (installationEntity == null) return null;

        return new GetInstallationResponse()
        {
            Name = installationEntity.Name,
            Branch = installationEntity.Branch.Name,
            IsDefault = installationEntity.IsDefault,
            LocalNumber = installationEntity.LocalNumber,
            Printer = installationEntity.Printer.Name
        };
    }

    public async Task<IEnumerable<GetInstallationResponse>> GetInstallationsAsync(int? branchId = null, CancellationToken token = default)
    {
        var entities = _db.Installations;

        if (branchId is not null)
        {
            entities = entities.Where(i => i.BranchId == branchId);
        }
        
        entities = entities
            .Include(i => i.Branch)
            .Include(i => i.Printer);
        
        return await entities.Select(installationEntity => new GetInstallationResponse() {
                Name = installationEntity.Name,
                Branch = installationEntity.Branch.Name,
                IsDefault = installationEntity.IsDefault,
                LocalNumber = installationEntity.LocalNumber,
                Printer = installationEntity.Printer.Name
            }).ToListAsync(token);
    }

    public async Task<AddInstallationResponse> AddNewInstallationAsync(AddInstallationRequest request, CancellationToken token = default)
    {
        // Validate parameters
        var branch = await _db.Branches.Where(b => b.Id == request.BranchId).FirstOrDefaultAsync(token);
        if (branch is null) return new AddInstallationResponse(null, $"Branch {request.BranchId} does not exist");
            
        var printer = await _db.Printers.Where(p => p.Id == request.PrinterId).FirstOrDefaultAsync(token);
        if (printer is null) return new AddInstallationResponse(null, $"Printer {request.PrinterId} does not exist");
        
        if (request.LocalNumber is null)
        {
            var nextNumber = await _db.Installations.MaxAsync(i => i.LocalNumber, cancellationToken: token) + 1;
            request.LocalNumber = (short?)nextNumber;
        }
        else
        {
            var isNumberPresent = _db.Installations.Any(i => i.LocalNumber == request.LocalNumber);
            if(isNumberPresent)
                return new AddInstallationResponse(null, $"Installation with provided local number already exists");
        }
        
        if (request.IsDefault)
        {
            var isInvalid = await _db.Installations.Where(i => i.BranchId == request.BranchId
                                                     && i.IsDefault).FirstOrDefaultAsync(token) is not null;
            if (isInvalid)
            {
                // Alternatively - we can just change default installation to the last created installation. 
                // But I am not sure that its better, maybe default printer device change is a sensitive operation
                // that has to be performed in a separate place. -> discuss with client what is desired.
                return new AddInstallationResponse(null, "Default installation already exists");
            }
        }
        else if (!await _db.Installations.AnyAsync(i => i.BranchId == request.BranchId, token)) 
        {
            // Same here. Maybe it would be better if we just set is_default to True instead of returning error. Not sure
            return new AddInstallationResponse(null, "First installation in the branch should be marked as default");
        }

        var entity = new InstallationEntity
        {
            BranchId = request.BranchId,
            PrinterId = request.PrinterId,
            ExternalId = Guid.NewGuid(),
            IsDefault = request.IsDefault,
            LocalNumber = (byte)request.LocalNumber,
            Name = request.Name,
        };
        
        var entry = await _db.AddAsync(entity, cancellationToken: token);
        await _db.SaveChangesAsync(token);
        return new AddInstallationResponse(entry.Entity.ExternalId);
    }

    public async Task<bool> DeleteInstallationAsync(Guid externalId, CancellationToken token = default)
    {
        var installationEntity = await _db.Installations
            .Where(i => i.ExternalId == externalId).FirstOrDefaultAsync(token);

        if (installationEntity == null) return false;

        _db.Remove(installationEntity);
        await _db.SaveChangesAsync(token);

        return true;
    }
}