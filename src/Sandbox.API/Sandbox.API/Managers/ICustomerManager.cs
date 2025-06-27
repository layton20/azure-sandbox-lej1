using Sandbox.API.Entities;
using Sandbox.Client.Models.Request;

namespace Sandbox.API.Managers;

public interface ICustomerManager
{
    Task<CustomerEntity> GetAsync(Guid uid);

    Task<List<CustomerEntity>> GetAsync(PagedRequest request);

    Task<SaveResult> CreateAsync(CustomerRequest request);

    Task<bool> DeleteAsync(Guid uid);

    Task<SaveResult> UpdateAsync(UpdateCustomerRequest request);

    Task<bool> DeleteAsync(List<Guid> uids);
}