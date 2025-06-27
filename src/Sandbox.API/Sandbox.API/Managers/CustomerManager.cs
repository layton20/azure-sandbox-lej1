using Sandbox.API.Adapters;
using Sandbox.API.Entities;
using Sandbox.API.Repositories;
using Sandbox.Client.Models.Request;

namespace Sandbox.API.Managers;

public class CustomerManager : ICustomerManager
{
    private readonly ICustomerRepository __CustomerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        __CustomerRepository = customerRepository;
    }

    public async Task<CustomerEntity> GetAsync(Guid uid)
    {
        return await __CustomerRepository.GetAsync(uid);
    }

    public async Task<List<CustomerEntity>> GetAsync(PagedRequest request)
    {
        return await __CustomerRepository.GetAsync(request.PageNumber, request.PageSize);
    }

    public async Task<SaveResult> CreateAsync(CustomerRequest request)
    {
        return await __CustomerRepository.CreateAsync(CustomerAdapter.ToEntity(request));
    }

    public async Task<bool> DeleteAsync(Guid uid)
    {
        return await __CustomerRepository.DeleteAsync(uid);
    }

    public async Task<SaveResult> UpdateAsync(UpdateCustomerRequest request)
    {
        return await __CustomerRepository.UpdateAsync(CustomerAdapter.ToEntity(request, request.Uid));
    }

    public async Task<bool> DeleteAsync(List<Guid> uids)
    {
        return await __CustomerRepository.DeleteAsync(uids);
    }
}