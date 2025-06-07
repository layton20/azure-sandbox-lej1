using Azure.Identity;

namespace Sandbox.API;

internal static class ConfigurationRegistry
{
    internal static void RegisterConfigurations(this ConfigurationManager manager)
    {
        manager.RegisterKeyVault();
    }

    private static ConfigurationManager RegisterKeyVault(this ConfigurationManager manager)
    {
        string _KeyVaultURL = "https://azure-sandbox-lej1-kv.vault.azure.net/";
        manager.AddAzureKeyVault(new Uri(_KeyVaultURL), new DefaultAzureCredential());

        return manager;
    }
}