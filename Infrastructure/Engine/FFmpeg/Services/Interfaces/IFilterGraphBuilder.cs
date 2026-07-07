using Domain.Definitions;
using Infrastructure.Engine.FFmpeg.Models;

namespace Infrastructure.Engine.FFmpeg.Services.Interfaces;

public interface IFilterGraphBuilder
{
    FilterGraph Build(Pipeline pipeline);
}
