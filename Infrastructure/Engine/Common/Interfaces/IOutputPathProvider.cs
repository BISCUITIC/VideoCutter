using Domain.Definitions;
using Domain.Processing;

namespace Infrastructure.Engine.Common.Interfaces;

public interface IOutputPathProvider
{
    string GetOutputPath(VideoProcessingDefinition definition,
                         VideoSegment segment,
                         int segmentNumber);
}
