using Application.Engine.Services.Interfaces;
using Domain.Processing;
using FFMpegCore;

namespace Infrastructure.Engine.FFmpeg.VideoMetadataReader;

public class FFmpegVideoMetadataReader : IVideoMetadataReader
{
    public async Task<VideoMetadata> ReadAsync(string inputFilePath, 
                                               CancellationToken cancellationToken = default)
    {
        IMediaAnalysis mediaInfo = await FFProbe.AnalyseAsync(inputFilePath);

        return new VideoMetadata(mediaInfo.Duration);
    }
}
