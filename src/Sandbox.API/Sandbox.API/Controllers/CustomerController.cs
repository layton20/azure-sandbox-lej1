using Microsoft.AspNetCore.Mvc;
using Sandbox.API.Adapters;
using Sandbox.API.Entities;
using Sandbox.API.Managers;
using Sandbox.API.Models.Request;
using Sandbox.API.Models.Response;

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
    public async Task<ActionResult> GetSampleCustomersAsync()
    {
        List<Customer> _Customers = await __CustomerManager.Get();

        return Ok(CustomerAdapter.ToResponse(_Customers));
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
}