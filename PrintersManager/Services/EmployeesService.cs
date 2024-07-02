using Database;
using Microsoft.EntityFrameworkCore;
using PrintersManager.Models;

namespace PrintersManager.Services;

public interface IEmployeesService
{
    Task<IEnumerable<GetEmployeesResponse>> GetEmployeesAsync(CancellationToken token = default);
}

public class EmployeesService : IEmployeesService
{
    
    private readonly PrintersDbContext _db;

    public EmployeesService(PrintersDbContext ctx)
    {
        _db = ctx;
    }
    
    public async Task<IEnumerable<GetEmployeesResponse>> GetEmployeesAsync(CancellationToken token = default)
    {
        var employeeEntities = _db.Employees;
        return await employeeEntities.Select(entity => new GetEmployeesResponse { Name = entity.Name }).ToListAsync(token);
    }
}