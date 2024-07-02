using Database.Records;

namespace Database.Entities;

public class InstallationEntity : InstallationRecord
{
    public BranchEntity Branch { get; set; }
    public PrinterEntity Printer { get; set; }
}