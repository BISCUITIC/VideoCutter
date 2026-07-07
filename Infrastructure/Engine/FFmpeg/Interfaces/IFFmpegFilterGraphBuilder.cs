using Domain.Definitions;
using Infrastructure.Engine.FFmpeg.Models;

namespace Infrastructure.Engine.FFmpeg.Interfaces;

public interface IFFmpegFilterGraphBuilder
{
    FilterGraph Build(Pipeline pipeline);
}
