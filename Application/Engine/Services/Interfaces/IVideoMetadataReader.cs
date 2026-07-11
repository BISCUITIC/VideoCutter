using Domain.Processing;

namespace Application.Engine.Services.Interfaces;

public interface IVideoMetadataReader
{
    public Task<VideoMetadata> ReadAsync(string inputFilePath);
}
