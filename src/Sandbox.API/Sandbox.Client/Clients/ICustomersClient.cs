using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.Client.Clients;

public interface ICustomersClient
{
    Task<List<CustomerResponse>> GetCustomersAsync(PagedRequest request);
    Task<CustomerResponse> GetCustomer(Guid uid);
    Task<bool> DeleteCustomerAsync(Guid uid);
    Task<CreateResponse> CreateCustomerAsync(CustomerRequest request);
    Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<bool> BulkDeleteCustomersAsync(List<Guid> uids);
}