using System.Text.Json;

namespace Sandbox.IntegrationTest.Extensions;

internal static class HttpResponseMessageExtensions
{
    public static async Task<T> GetContent<T>(this HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        Stream _Content = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(_Content);
    }
}