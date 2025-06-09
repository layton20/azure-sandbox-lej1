using Sandbox.API.Entities;

namespace Sandbox.API.Repositories;

public interface ICustomerRepository
{
    Task<CustomerEntity> Get(Guid uid);

    Task<List<CustomerEntity>> Get();
    Task<SaveResult> Create(CustomerEntity customer);
    Task<bool> Delete(Guid uid);
    Task<SaveResult> Update(CustomerEntity customer);
    Task<bool> Delete(List<Guid> uids);
}