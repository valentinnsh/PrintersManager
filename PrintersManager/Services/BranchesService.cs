using Database;
using Microsoft.EntityFrameworkCore;
using PrintersManager.Models;

namespace PrintersManager.Services;

public interface IBranchesService
{
    Task<IEnumerable<GetBranchesResponse>> GetBranchesAsync(CancellationToken token = default);
}

public class BranchesService : IBranchesService
{
    
    private readonly PrintersDbContext _db;

    public BranchesService(PrintersDbContext ctx)
    {
        _db = ctx;
    }
    
    public async Task<IEnumerable<GetBranchesResponse>> GetBranchesAsync(CancellationToken token = default)
    {
        var branchEntities = _db.Branchs;
        return await branchEntities.Select(entity => new GetBranchesResponse
        {
            Name = entity.Name, Location = entity.Location
        }).ToListAsync(token);
    }
}