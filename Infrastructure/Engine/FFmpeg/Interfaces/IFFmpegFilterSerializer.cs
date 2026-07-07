using Domain.Filters.Interfaces;

namespace Infrastructure.Engine.FFmpeg.Interfaces;

public interface IFFmpegFilterSerializer
{
    string Serialize(IFilter filter);
}
