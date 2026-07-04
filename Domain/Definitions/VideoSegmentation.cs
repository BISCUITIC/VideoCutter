namespace Domain.Definitions;

public class VideoSegmentation
{
    public TimeSpan ChunkDuration { get; init; }
    public TimeSpan Offset { get; init; }
    public int? MaxChunks { get; init; }

    public VideoSegmentation(TimeSpan chunkDuration, TimeSpan offset, int? maxChunks)
    {
        ChunkDuration = chunkDuration;
        Offset = offset;
        MaxChunks = maxChunks;
    }
}
