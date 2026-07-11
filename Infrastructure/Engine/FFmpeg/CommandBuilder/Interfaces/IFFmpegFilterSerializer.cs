using Domain.Filters.Interfaces;

namespace Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;

public interface IFFmpegFilterSerializer
{
    string Serialize(IFilter filter);
}
