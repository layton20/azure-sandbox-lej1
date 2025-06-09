namespace Sandbox.Client.Models.Response;

public class BaseModelResponse
{
    public Guid Uid { get; set; }
    public DateTime CreateTimeStamp { get; set; }
    public DateTime AmendTimeStamp { get; set; }
}