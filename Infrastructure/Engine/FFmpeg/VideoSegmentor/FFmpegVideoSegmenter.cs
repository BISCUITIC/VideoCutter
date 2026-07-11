using Application.Engine.Services.Interfaces;
using Domain.Definitions;
using Domain.Processing;

namespace Infrastructure.Engine.FFmpeg.VideoSegmentor;

public class FFmpegVideoSegmenter : IVideoSegmenter
{
    public IReadOnlyCollection<VideoSegment> Process(VideoSegmentation segmentation, 
                                                     VideoMetadata metadata)
    {
        List<VideoSegment> segments = new List<VideoSegment>();  

        throw new NotImplementedException();
    }

    private int CalculateMaxChunks(VideoSegmentation segmentation, 
                                   VideoMetadata metadata)
    {      
        if (segmentation.MaxChunks is not null)
            return segmentation.MaxChunks.Value;   

        throw new NotImplementedException();
    }
}
