using System.ComponentModel.DataAnnotations;

namespace Sandbox.API.Entities;

public class Customer : BaseEntity
{
    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    [Required] public string Address { get; set; } = string.Empty;

    [Required] public string PostalCode { get; set; } = string.Empty;

    [EmailAddress] public string Email { get; set; } = string.Empty;
}