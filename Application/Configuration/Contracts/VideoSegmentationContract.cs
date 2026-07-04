namespace Application.Configuration.Contracts;

public class VideoSegmentationContract
{
    public TimeSpan ChunkDuration { get; }
    public TimeSpan? Offset { get; }
    public int? MaxChunks { get; }

    public VideoSegmentationContract(TimeSpan chunkDuration, TimeSpan? offset, int? maxChunks)
    {
        ChunkDuration = chunkDuration;
        Offset = offset;
        MaxChunks = maxChunks;
    }
}
