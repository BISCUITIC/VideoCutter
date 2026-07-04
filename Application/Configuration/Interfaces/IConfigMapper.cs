using Application.Configuration.Contracts;
using Domain;

namespace Application.Configuration.Interfaces;

public interface IConfigMapper
{
    VideoProcessingDefinition Map(ConfigContract config);
}
