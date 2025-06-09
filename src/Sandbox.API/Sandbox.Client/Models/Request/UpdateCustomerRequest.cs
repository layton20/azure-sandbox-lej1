namespace Sandbox.Client.Models.Request;

public class UpdateCustomerRequest : CustomerRequest
{
    public Guid Uid { get; set; }
}