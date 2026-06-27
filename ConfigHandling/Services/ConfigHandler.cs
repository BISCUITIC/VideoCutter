using Config.Contracts;
using System.Text.Json;

namespace Config.Services;

public class ConfigHandler
{
    private readonly string _configPath;
    private readonly JsonSerializerOptions _options;

    public ConfigHandler(string configPath)
    {
        _configPath = configPath;

        _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
        };
    }

    public ConfigPipline Load()
    {
        string json = File.ReadAllText(_configPath);        

        ConfigPipline? configData = JsonSerializer.Deserialize<ConfigPipline>(json, _options);

        if (configData is null)
            throw new NullReferenceException("Failed to deserialize config");

        return configData;
    }
}
