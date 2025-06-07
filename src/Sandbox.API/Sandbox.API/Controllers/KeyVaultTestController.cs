using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sandbox.API.Settings;

namespace Sandbox.API.Controllers;

public class KeyVaultTestController : Controller
{
    private readonly IOptions<GlobalSettings> __Settings;

    public KeyVaultTestController(IOptions<GlobalSettings> settings)
    {
        __Settings = settings;
    }

    [HttpGet("GetSampleSecret")]
    public IActionResult GetSampleSecret()
    {
        string _SampleValue = __Settings.Value.SampleKey;

        return string.IsNullOrWhiteSpace(_SampleValue)
            ? NotFound("Secret not found.")
            : Ok($"Secret value: {_SampleValue}");
    }
}