namespace PrintersManager.Models;

public class GetInstallationResponse
{
    public string Name { get; set; }
    public short LocalNumber { get; set; }
    public bool IsDefault { get; set; }
    public string Printer { get; set; }
    public string Branch { get; set; }
}