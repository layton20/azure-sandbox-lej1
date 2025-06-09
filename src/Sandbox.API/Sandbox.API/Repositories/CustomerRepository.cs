using Microsoft.EntityFrameworkCore;
using Sandbox.API.Data;
using Sandbox.API.Entities;
using Sandbox.API.Extensions;

namespace Sandbox.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext __Context;

    public CustomerRepository(ApplicationDbContext context)
    {
        __Context = context;
    }

    public async Task<CustomerEntity> Get(Guid uid)
    {
        return await __Context.Customers.FindAsync(uid) ??
               throw new KeyNotFoundException($"Customer with UID {uid} not found.");
    }

    public async Task<List<CustomerEntity>> Get()
    {
        return await __Context.Customers.ToListAsync() ??
               throw new InvalidOperationException("No customers found in the database.");
    }

    public async Task<SaveResult> Create(CustomerEntity customer)
    {
        if (await __Context.Customers.AnyAsync(c => c.Email == customer.Email))
            return SaveResult.Duplicate(customer.Uid);

        customer.Uid = Guid.NewGuid();
        customer.CreateTimeStamp = DateTime.UtcNow;
        customer.AmendTimeStamp = DateTime.UtcNow;

        await __Context.Customers.AddAsync(customer);

        int _Changes = await __Context.SaveChangesAsync();

        return _Changes > 0 ? SaveResult.Success(customer.Uid) : SaveResult.Failure();
    }

    public async Task<bool> Delete(Guid uid)
    {
        CustomerEntity? _Customer = await __Context.Customers.FirstOrDefaultAsync(c => c.Uid == uid);

        if (_Customer == null) return false;

        __Context.Customers.Remove(_Customer);

        return await __Context.SaveChangesAsync() > 0;
    }

    public async Task<SaveResult> Update(CustomerEntity customer)
    {
        CustomerEntity? _Customer = await __Context.Customers.FirstOrDefaultAsync(c => c.Uid == customer.Uid);

        if (_Customer == null) return SaveResult.Failure();

        _Customer.FirstName = customer.FirstName;
        _Customer.LastName = customer.LastName;
        _Customer.DateOfBirth = customer.DateOfBirth;
        _Customer.Address = customer.Address;
        _Customer.PostalCode = customer.PostalCode;
        _Customer.Email = customer.Email;
        _Customer.AmendTimeStamp = DateTime.UtcNow;

        return await __Context.SaveChangesAsync() > 0
            ? SaveResult.Success(_Customer.Uid)
            : SaveResult.Failure();
    }

    public async Task<bool> Delete(List<Guid> uids)
    {
        List<CustomerEntity> _Customers = await __Context.Customers.Where(c => uids.Contains(c.Uid)).ToListAsync();

        if (_Customers.IsEmpty()) return false;

        __Context.Customers.RemoveRange(_Customers);

        return await __Context.SaveChangesAsync() > 0;
    }
}