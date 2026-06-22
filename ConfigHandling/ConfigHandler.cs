using Config.Contracts;
using System.Text.Json;

namespace Config;

public class ConfigHandler
{
    private readonly string _configPath;

    public ConfigHandler(string configPath)
    {
        _configPath = configPath;
    }

    public ConfigData Handle()
    {
        string json = File.ReadAllText(_configPath);
        Console.WriteLine(json);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
        };

        ConfigData? configData = JsonSerializer.Deserialize<ConfigData>(json, options);

        if (configData is null)
            throw new NullReferenceException("Failed to deserialize sonfig");

        return configData;
    }
}
