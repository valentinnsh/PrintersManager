using Database.Records;

namespace Database.Entities;

public class PrinterEntity : PrinterRecord
{
    public ICollection<InstallationEntity> Installations { get; set; }
}