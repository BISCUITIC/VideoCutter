using Application.Configuration.Contracts;
using Application.Configuration.Interfaces;
using Domain;

namespace Infrastructure.Configuration.Json.Services;

public class ConfigJsonMapper : IConfigMapper
{
    public VideoProcessingDefinition Map(ConfigContract config)
    {        
        return new VideoProcessingDefinition();
    }
}
