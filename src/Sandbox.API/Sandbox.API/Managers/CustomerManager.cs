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

    public async Task<CustomerEntity> Get(Guid uid)
    {
        return await __CustomerRepository.Get(uid);
    }

    public async Task<List<CustomerEntity>> Get()
    {
        return await __CustomerRepository.Get();
    }

    public async Task<SaveResult> Create(CustomerRequest request)
    {
        return await __CustomerRepository.Create(CustomerAdapter.ToEntity(request));
    }

    public async Task<bool> Delete(Guid uid)
    {
        return await __CustomerRepository.Delete(uid);
    }

    public async Task<SaveResult> Update(UpdateCustomerRequest request)
    {
        return await __CustomerRepository.Update(CustomerAdapter.ToEntity(request, request.Uid));
    }

    public async Task<bool> Delete(List<Guid> uids)
    {
        return await __CustomerRepository.Delete(uids);
    }
}