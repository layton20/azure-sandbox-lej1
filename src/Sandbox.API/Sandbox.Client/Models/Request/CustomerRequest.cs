using System.ComponentModel.DataAnnotations;

namespace Sandbox.Client.Models.Request;

public class CustomerRequest
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Address { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string PostalCode { get; set; }
    public DateTime DateOfBirth { get; set; }
}