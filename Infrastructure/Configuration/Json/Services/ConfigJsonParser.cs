using System.Text.Json;
using Application.Configuration.Interfaces;
using Application.Configuration.Contracts;

namespace Infrastructure.Configuration.Json.Services;

public class ConfigJsonParser : IConfigParser
{
    private readonly JsonSerializerOptions _options;

    public ConfigJsonParser(JsonSerializerOptions options)
    {
        _options = options;
    }

    public ConfigContract Deserialize(string json)
    {
        ConfigContract? config = JsonSerializer.Deserialize<ConfigContract>(json, _options);       

        if (config == null) 
            throw new JsonException($"Failed to deserialize config");

        return config;
    }
}
