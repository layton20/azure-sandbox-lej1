namespace Sandbox.API.Settings;

public class GlobalSettings
{
    public const string AppSettingsSection = "AppSettings";
    public string SampleKey { get; set; }
    public DatabaseSettings Database { get; set; } = new();
    public AppInsights AppInsights { get; set; } = new();
}