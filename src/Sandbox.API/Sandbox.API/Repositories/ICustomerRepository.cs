using Sandbox.API.Entities;

namespace Sandbox.API.Repositories;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetAsync(Guid uid);

    Task<List<CustomerEntity>> GetAsync();
    Task<SaveResult> CreateAsync(CustomerEntity customer);
    Task<bool> DeleteAsync(Guid uid);
    Task<SaveResult> UpdateAsync(CustomerEntity customer);
    Task<bool> DeleteAsync(List<Guid> uids);
}