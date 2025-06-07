using Sandbox.API.Entities;

namespace Sandbox.API.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Get(Guid uid);

    Task<List<Customer>> Get();
    Task<SaveResult> Create(Customer customer);
    Task<bool> Delete(Guid uid);
}