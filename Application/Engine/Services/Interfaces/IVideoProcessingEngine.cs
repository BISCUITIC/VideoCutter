using Domain.Definitions;

namespace Application.Engine.Services.Interfaces;

public interface IVideoProcessingEngine
{
    Task ProcessingAsync(VideoProcessing definition, 
                         CancellationToken cancellationToken = default);
}
