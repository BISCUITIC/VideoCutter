using Domain.Filters;
using Domain.Filters.Interfaces;
using Infrastructure.Engine.FFmpeg.Interfaces;

namespace Infrastructure.Engine.FFmpeg.Services;

public class FFmpegFilterSerializer : IFFmpegFilterSerializer
{
    public string Serialize(IFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return filter switch
        {
            ResizeFilter resizeFilter =>
                $"scale={resizeFilter.Width}:{resizeFilter.Height}",

            _ => throw new NotSupportedException(
                $"FFmpeg serialization for filter " +
                $"'{filter.GetType().Name}' is not implemented."
            )
        };
    }
}