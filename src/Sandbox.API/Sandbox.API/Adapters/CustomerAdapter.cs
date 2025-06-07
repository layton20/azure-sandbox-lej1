using Sandbox.API.Entities;
using Sandbox.API.Models.Request;
using Sandbox.API.Models.Response;

namespace Sandbox.API.Adapters;

internal static class CustomerAdapter
{
    internal static CustomerResponse ToResponse(Customer? customer)
    {
        return customer == null
            ? null
            : new CustomerResponse
            {
                Uid = customer.Uid,
                Address = customer.Address,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PostalCode = customer.PostalCode,
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email
            };
    }

    internal static Customer ToResponse(CustomerRequest request)
    {
        return new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            Email = request.Email,
            PostalCode = request.PostalCode,
            DateOfBirth = request.DateOfBirth
        };
    }

    internal static List<CustomerResponse> ToResponse(List<Customer> customers)
    {
        return customers?.Count > 0 ? customers.ConvertAll(ToResponse) : [];
    }
}