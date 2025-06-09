using Microsoft.AspNetCore.Mvc;
using Sandbox.API.Adapters;
using Sandbox.API.Entities;
using Sandbox.API.Managers;
using Sandbox.Client.Models.Request;
using Sandbox.Client.Models.Response;

namespace Sandbox.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerManager __CustomerManager;

    public CustomerController(ICustomerManager customerManager)
    {
        __CustomerManager = customerManager;
    }

    [HttpGet]
    public async Task<ActionResult> GetCustomersAsync()
    {
        List<CustomerEntity> _Customers = await __CustomerManager.Get();

        return Ok(CustomerAdapter.ToResponse(_Customers));
    }

    [HttpGet("{uid}")]
    public async Task<IActionResult> GetAsync(Guid uid)
    {
        CustomerEntity _Customer = await __CustomerManager.Get(uid);

        return _Customer != null ? Ok(CustomerAdapter.ToResponse(_Customer)) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerRequest request)
    {
        SaveResult _Result = await __CustomerManager.Create(request);

        if (_Result.IsDuplicate) return Conflict(_Result);

        return _Result.IsSuccess ? Ok(CreateResponse.Success(_Result.Uid)) : BadRequest(CreateResponse.Failure());
    }

    [HttpDelete("{uid}")]
    public async Task<IActionResult> DeleteAsync(Guid uid)
    {
        bool _Result = await __CustomerManager.Delete(uid);

        return _Result ? Ok(_Result) : NotFound();
    }

    [HttpPost("BulkDelete")]
    public async Task<IActionResult> BulkDeleteAsync([FromBody] List<Guid> uids)
    {
        bool _Result = await __CustomerManager.Delete(uids);

        return _Result ? Ok(_Result) : NotFound();
    }

    [HttpPut("{uid}")]
    public async Task<IActionResult> UpdateAsync(Guid uid, [FromBody] UpdateCustomerRequest request)
    {
        SaveResult _Result = await __CustomerManager.Update(request);

        return _Result.IsSuccess ? Ok(true) : _Result.IsDuplicate ? Conflict(_Result) : NotFound();
    }
}