using System.Net.Http.Json;
using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.Client.Clients;

public class CustomerClient : ICustomerClient
{
    private const string BASE_URL = "/api/customers/";
    private readonly HttpClient __HttpClient;

    public CustomerClient(HttpClient httpClient)
    {
        __HttpClient = httpClient;
    }

    public async Task<List<CustomerResponse>> GetCustomersAsync()
    {
        return await __HttpClient.GetFromJsonAsync<List<CustomerResponse>>(BASE_URL);
    }

    public async Task<CustomerResponse> GetCustomer(Guid uid)
    {
        return await __HttpClient.GetFromJsonAsync<CustomerResponse>($"{BASE_URL}{uid}");
    }

    public async Task<bool> DeleteCustomerAsync(Guid uid)
    {
        return await __HttpClient.DeleteAsync($"{BASE_URL}{uid}").ContinueWith(t => t.Result.IsSuccessStatusCode);
    }

    public async Task<CreateResponse> CreateCustomerAsync(CustomerRequest request)
    {
        return await __HttpClient.PostAsJsonAsync($"{BASE_URL}", request)
            .ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
                return t.Result.Content.ReadFromJsonAsync<CreateResponse>();
            })
            .Unwrap();
    }

    public async Task<bool> BulkDeleteCustomersAsync(List<Guid> uids)
    {
        return await __HttpClient.PostAsJsonAsync($"{BASE_URL}BulkDelete", uids)
            .ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
                return t.Result.Content.ReadFromJsonAsync<bool>();
            })
            .Unwrap();
    }

    public async Task<bool> UpdateCustomerAsync(UpdateCustomerRequest request)
    {
        HttpResponseMessage _Response = await __HttpClient.PutAsJsonAsync($"{BASE_URL}{request.Uid}", request);
        _Response.EnsureSuccessStatusCode();
        return true;
    }
}