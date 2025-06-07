using Sandbox.API.Adapters;
using Sandbox.API.Entities;
using Sandbox.API.Models.Request;
using Sandbox.API.Repositories;

namespace Sandbox.API.Managers;

public class CustomerManager : ICustomerManager
{
    private readonly ICustomerRepository __CustomerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        __CustomerRepository = customerRepository;
    }

    public async Task<Customer> Get(Guid uid)
    {
        return await __CustomerRepository.Get(uid);
    }

    public async Task<List<Customer>> Get()
    {
        return await __CustomerRepository.Get();
    }

    public async Task<SaveResult> Create(CustomerRequest request)
    {
        return await __CustomerRepository.Create(CustomerAdapter.ToResponse(request));
    }

    public async Task<bool> Delete(Guid uid)
    {
        return await __CustomerRepository.Delete(uid);
    }
}