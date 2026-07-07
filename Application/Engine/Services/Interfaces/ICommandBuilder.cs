using Domain.Definitions;
using Domain.Engine;

namespace Application.Engine.Services.Interfaces;

public interface ICommandBuilder
{
    Command Build(VideoSegment segment, VideoProcessingDefinition definition);
}