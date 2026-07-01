using System.Text.Json;
using Infrastructure.Configuration.Contracts;

namespace Infrastructure.Configuration.Services;

public class ConfigJsonParser
{
    private readonly JsonSerializerOptions _options;

    public ConfigJsonParser(JsonSerializerOptions options)
    {
        _options = options;
    }

    public Config Deserialize(string json)
    {
        Config? config = JsonSerializer.Deserialize<Config>(json, _options);       

        if (config == null) 
            throw new JsonException($"Failed to deserialize config");

        return config;
    }
}
