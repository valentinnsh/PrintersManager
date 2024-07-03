namespace PrintersManager.Models;

public class AddInstallationRequest
{
    public string Name { get; set; }
    public int BranchId { get; set; }
    public int PrinterId { get; set; }
    public short? LocalNumber { get; set; }
    public bool IsDefault { get; set; }
}