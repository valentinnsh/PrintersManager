using Database.Records;

namespace Database.Entities;

public class EmployeeEntity : EmployeeRecord
{
    public BranchEntity Branch { get; set; }
}