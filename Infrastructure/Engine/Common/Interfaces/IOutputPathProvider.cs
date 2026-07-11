using Domain.Definitions;
using Domain.Processing;

namespace Infrastructure.Engine.Common.Interfaces;

public interface IOutputPathProvider
{
    string GetOutputPath(int segmentNumber,
                         VideoProcessingDefinition definition,
                         VideoSegment segment);
}
