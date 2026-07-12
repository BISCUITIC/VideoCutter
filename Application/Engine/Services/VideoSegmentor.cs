using Application.Engine.Services.Interfaces;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services;

public class VideoSegmentor : IVideoSegmentor
{
    private int CalculateMaxChunks(VideoSegmentation segmentation,
                                   VideoMetadata metadata)
    {
        TimeSpan videoDuration = metadata.Duration;
        TimeSpan offset = segmentation.Offset;
        TimeSpan chunkDuration = segmentation.ChunkDuration;

        int chunksCount = (int)Math.Ceiling((videoDuration - offset) / chunkDuration);

        if (segmentation.MaxChunks is not null && segmentation.MaxChunks < chunksCount)
            return segmentation.MaxChunks.Value;

        return chunksCount;
    }

    private VideoSegment CreateSegment(int index,
                                       TimeSpan videoDuration,
                                       TimeSpan offset,
                                       TimeSpan chunkDuration)
    {
        TimeSpan segmentStart = offset + index * chunkDuration;
        TimeSpan segmentEnd;

        if (segmentStart + chunkDuration < videoDuration)
            segmentEnd = segmentStart + chunkDuration;
        else
            segmentEnd = videoDuration;

        return new VideoSegment(segmentStart, segmentEnd);
    }

    public IReadOnlyCollection<VideoSegment> Process(VideoSegmentation segmentation,
                                                     VideoMetadata metadata)
    {
        List<VideoSegment> segments = new List<VideoSegment>();
        int chunksCount = CalculateMaxChunks(segmentation, metadata);

        for (int index = 0; index < chunksCount; index++)
        {
            segments.Add(
                CreateSegment(
                    index,
                    metadata.Duration,
                    segmentation.Offset,
                    segmentation.ChunkDuration
                )
            );
        }

        return segments.AsReadOnly();
    }
}
