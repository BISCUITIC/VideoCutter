using Domain.Definitions;

namespace Application.Engine.Services.Interfaces;

public interface IVideoProcessingEngine
{
    Task ProcessingAsync(VideoProcessingDefinition definition, 
                         CancellationToken cancellationToken = default);
}
