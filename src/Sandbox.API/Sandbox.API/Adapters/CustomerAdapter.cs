using Sandbox.API.Entities;
using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.API.Adapters;

internal static class CustomerAdapter
{
    internal static CustomerResponse ToResponse(CustomerEntity? customer)
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
                Email = customer.Email,
                AmendTimeStamp = customer.AmendTimeStamp,
                CreateTimeStamp = customer.CreateTimeStamp
            };
    }

    internal static CustomerEntity ToEntity(CustomerRequest request)
    {
        return new CustomerEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            Email = request.Email,
            PostalCode = request.PostalCode,
            DateOfBirth = request.DateOfBirth
        };
    }

    internal static CustomerEntity ToEntity(CustomerRequest request, Guid uid)
    {
        CustomerEntity _Customer = ToEntity(request);
        _Customer.Uid = uid;
        return _Customer;
    }

    internal static List<CustomerResponse> ToResponse(List<CustomerEntity> customers)
    {
        return customers?.Count > 0 ? customers.ConvertAll(ToResponse) : [];
    }
}