using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Sandbox.API.Controllers
{
    public class KeyVaultTestController : Controller
    {
        private readonly IOptions<GlobalSettings> __Settings;
        private readonly string _SampleKey;

        public KeyVaultTestController(IOptions<GlobalSettings> settings) => __Settings = settings;

        [HttpGet("GetSampleSecret")]
        public IActionResult GetSampleSecret()
        {
            string _SampleValue = __Settings.Value.SampleKey;

            return string.IsNullOrWhiteSpace(_SampleValue)
                ? NotFound("Secret not found.")
                : Ok($"Secret value: {_SampleValue}");
        }
    }
}