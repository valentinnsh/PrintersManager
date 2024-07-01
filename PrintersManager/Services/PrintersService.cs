using Database;
using Database.Entities;
using Database.Records;
using Microsoft.EntityFrameworkCore;
using PrintersManager.Models;

namespace PrintersManager.Services;

public interface IPrintersService
{
    Task<IEnumerable<PrinterEntity>> GetPrintersAsync(ConnectionTypes? connectionType = null, CancellationToken token = default);
}

public class PrintersService : IPrintersService
{
    
    private readonly PrintersDbContext _db;

    public PrintersService(PrintersDbContext ctx)
    {
        _db = ctx;
    }
    
    public async Task<IEnumerable<PrinterEntity>> GetPrintersAsync(ConnectionTypes? connectionType = null, CancellationToken token = default)
    {
        var printerEntities = _db.Printers;

        if (connectionType is not null)
        {
            printerEntities = printerEntities.Where(p => p.ConnectionType == connectionType);
        }
        return await printerEntities.ToListAsync(token);
    }
}