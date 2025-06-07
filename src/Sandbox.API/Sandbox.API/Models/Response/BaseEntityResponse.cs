namespace Sandbox.API.Models.Response;

public class BaseEntityResponse
{
    public Guid Uid { get; set; }
    public DateTime CreateTimeStamp { get; set; }
    public DateTime AmendTimeStamp { get; set; }
}