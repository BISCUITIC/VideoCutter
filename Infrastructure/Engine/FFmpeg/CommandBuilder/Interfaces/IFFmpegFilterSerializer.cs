using Domain.Filters.Interfaces;

namespace Infrastructure.Engine.FFmpeg.CommandBuilder.Interfaces;

public interface IFFmpegFilterSerializer
{
    string Serialize(IFilter filter);
}
