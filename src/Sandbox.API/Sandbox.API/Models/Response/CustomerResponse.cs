namespace Sandbox.API.Models.Response;

public class CustomerResponse : BaseEntityResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
}