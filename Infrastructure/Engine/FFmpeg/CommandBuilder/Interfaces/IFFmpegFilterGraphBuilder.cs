using Domain.Definitions;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;

public interface IFFmpegFilterGraphBuilder
{
    FilterGraph Build(Pipeline pipeline);
}
