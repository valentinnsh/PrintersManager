using Database.Records;

namespace Database.Entities;

public class BranchEntity : BranchRecord
{
    public ICollection<InstallationEntity> Installations { get; set; }
    public ICollection<EmployeeEntity> Employees { get; set; }
}