using Application.Configuration.Contracts;
using Domain.Definitions;

namespace Application.Configuration.Interfaces;

public interface IConfigMapper
{
    VideoProcessingDefinition Map(ConfigContract config);
}
