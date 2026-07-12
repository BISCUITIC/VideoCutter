using Application.Configuration.Contracts;
using Domain.Definitions;

namespace Application.Configuration.Interfaces;

public interface IConfigMapper
{
    VideoProcessing Map(ConfigContract config);
}
