using System.Collections.Concurrent;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sandbox.Client.Clients;
using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.IntegrationTest.Controllers;

[TestClass]
public class CustomerControllerTest : BaseTest
{
    private const string UPDATED = "Updated";
    private readonly ConcurrentBag<Guid> __Cleanup_Customer_UIDs = [];
    private readonly CustomerClient __Client;

    public CustomerControllerTest()
    {
        __Client = GetClient<CustomerClient>();
    }

    private async Task<CreateResponse> CreateCustomerAsync(CustomerRequest request)
    {
        CreateResponse _Response = await __Client.CreateCustomerAsync(request);

        if (_Response.IsSuccess && _Response.Uid != Guid.Empty) __Cleanup_Customer_UIDs.Add(_Response.Uid);

        return _Response;
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        if (!__Cleanup_Customer_UIDs.IsEmpty)
        {
            await __Client.BulkDeleteCustomersAsync(__Cleanup_Customer_UIDs.ToList());
            __Cleanup_Customer_UIDs.Clear();
        }
    }


    [TestMethod]
    public async Task CustomerController_GetCustomers_ShouldReturnCustomersAsync()
    {
        int _PageSize = 5;

        Faker<CustomerRequest>? _CustomerFaker = new Faker<CustomerRequest>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

        List<Guid> _Customer_UIDs = (await Task.WhenAll(
            Enumerable.Range(0, _PageSize).Select(_ => CreateCustomerAsync(_CustomerFaker.Generate()))
        )).Select(x => x.Uid).ToList();

        List<CustomerResponse> _GetCustomers = await __Client.GetCustomersAsync();

        Assert.IsNotNull(_GetCustomers);
        Assert.AreEqual(_PageSize, _GetCustomers.Count);
        CollectionAssert.AreEquivalent(_Customer_UIDs, _GetCustomers.ConvertAll(x => x.Uid));
    }

    [TestMethod]
    public async Task CustomerController_GetCustomer_ShouldReturnCustomerAsync()
    {
        Faker<CustomerRequest>? _CustomerFaker = new Faker<CustomerRequest>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

        CustomerRequest? _CustomerRequest = _CustomerFaker.Generate();

        CreateResponse _Customer = await CreateCustomerAsync(_CustomerRequest);

        CustomerResponse? _GetCustomer = await __Client.GetCustomer(_Customer.Uid);

        Assert.IsNotNull(_GetCustomer);
        Assert.AreEqual(_Customer.Uid, _GetCustomer.Uid);
    }

    [TestMethod]
    public async Task CustomerController_UpdateCustomer_ShouldReturnTrueAsync()
    {
        Faker<CustomerRequest>? _CustomerFaker = new Faker<CustomerRequest>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

        CustomerRequest? _CustomerRequest = _CustomerFaker.Generate();

        CreateResponse _CreateResponse = await CreateCustomerAsync(_CustomerRequest);

        UpdateCustomerRequest _UpdateCustomerRequest = new()
        {
            Uid = _CreateResponse.Uid,
            FirstName = UPDATED,
            LastName = _CustomerRequest.LastName,
            Email = _CustomerRequest.Email,
            Address = _CustomerRequest.Address,
            PostalCode = _CustomerRequest.PostalCode,
            DateOfBirth = _CustomerRequest.DateOfBirth
        };

        bool _UpdateCustomer = await __Client.UpdateCustomerAsync(_UpdateCustomerRequest);

        Assert.IsTrue(_UpdateCustomer);

        CustomerResponse? _GetCustomer = await __Client.GetCustomer(_CreateResponse.Uid);

        Assert.AreEqual(_CreateResponse.Uid, _GetCustomer.Uid);
        Assert.AreEqual(UPDATED, _GetCustomer.FirstName);
    }

    [TestMethod]
    public async Task CustomerController_DeleteCustomer_ShouldReturnTrueAsync()
    {
        Faker<CustomerRequest>? _CustomerFaker = new Faker<CustomerRequest>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

        CustomerRequest? _CustomerRequest = _CustomerFaker.Generate();

        CreateResponse _CreateResponse = await CreateCustomerAsync(_CustomerRequest);

        bool _DeleteCustomer = await __Client.DeleteCustomerAsync(_CreateResponse.Uid);

        Assert.IsTrue(_DeleteCustomer);

        CustomerResponse? _GetCustomer = await __Client.GetCustomer(_CreateResponse.Uid);

        Assert.IsNull(_GetCustomer);
    }

    [TestMethod]
    public async Task CustomerController_BulkDeleteCustomers_ShouldReturnTrueAsync()
    {
        Faker<CustomerRequest>? _CustomerFaker = new Faker<CustomerRequest>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
            .RuleFor(x => x.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)));

        List<Guid> _Customer_UIDs = (await Task.WhenAll(
                Enumerable.Range(0, 5).Select(_ => CreateCustomerAsync(_CustomerFaker.Generate())))).Select(x => x.Uid)
            .ToList();

        bool _BulkDeleteCustomers = await __Client.BulkDeleteCustomersAsync(_Customer_UIDs);

        Assert.IsTrue(_BulkDeleteCustomers);
    }

    [TestMethod]
    public async Task CustomerController_BulkDeleteCustomers_ShouldReturnTrueAsync_WithEmptyList()
    {
        bool _BulkDeleteCustomers = await __Client.BulkDeleteCustomersAsync([]);

        Assert.IsTrue(_BulkDeleteCustomers);
    }
}