using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services.Interfaces;

public interface ICommandBuilder
{
    Command Build(VideoSegment segment, VideoProcessingDefinition definition);
}