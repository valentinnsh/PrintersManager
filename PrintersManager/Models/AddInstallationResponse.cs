namespace PrintersManager.Models;

public class AddInstallationResponse
{
    public Guid? InstallationId { get; set; }
    public string? ErrorMessage { get; set; }

    public AddInstallationResponse(Guid? installationId, string? errorMessage = null)
    {
        InstallationId = installationId;
        ErrorMessage = errorMessage;
    }
}