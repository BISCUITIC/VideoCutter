namespace Infrastructure.Configuration.Contracts;

public class VideoSegmentation
{
    public TimeSpan ChunkDuration { get; }
    public TimeSpan? Offset { get; }
    public int? MaxChunks { get; }

    public VideoSegmentation(TimeSpan chunkDuration, TimeSpan? offset, int? maxChunks)
    {
        ChunkDuration = chunkDuration;
        Offset = offset;
        MaxChunks = maxChunks;
    }
}
