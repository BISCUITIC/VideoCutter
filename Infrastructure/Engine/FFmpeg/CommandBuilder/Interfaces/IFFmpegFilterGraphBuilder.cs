using Domain.Definitions;
using Infrastructure.Engine.FFmpeg.CommandBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommandBuilder.Interfaces;

public interface IFFmpegFilterGraphBuilder
{
    FilterGraph Build(Pipeline pipeline);
}
