using Sandbox.API.Entities;
using Sandbox.API.Models.Request;

namespace Sandbox.API.Managers;

public interface ICustomerManager
{
    Task<Customer> Get(Guid uid);

    Task<List<Customer>> Get();

    Task<SaveResult> Create(CustomerRequest request);
    Task<bool> Delete(Guid uid);
}