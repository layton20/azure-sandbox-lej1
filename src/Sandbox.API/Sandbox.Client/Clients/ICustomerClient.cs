using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.Client.Clients;

public interface ICustomerClient
{
    Task<List<CustomerResponse>> GetCustomersAsync();
    Task<CustomerResponse> GetCustomer(Guid uid);
    Task<bool> DeleteCustomerAsync(Guid uid);
    Task<CreateResponse> CreateCustomerAsync(CustomerRequest request);
    Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<bool> BulkDeleteCustomersAsync(List<Guid> uids);
}