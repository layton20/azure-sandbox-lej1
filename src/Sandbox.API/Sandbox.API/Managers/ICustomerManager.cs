using Sandbox.API.Entities;
using Sandbox.Client.Models.Request;

namespace Sandbox.API.Managers;

public interface ICustomerManager
{
    Task<CustomerEntity> Get(Guid uid);

    Task<List<CustomerEntity>> Get();

    Task<SaveResult> Create(CustomerRequest request);

    Task<bool> Delete(Guid uid);

    Task<SaveResult> Update(UpdateCustomerRequest request);

    Task<bool> Delete(List<Guid> uids);
}