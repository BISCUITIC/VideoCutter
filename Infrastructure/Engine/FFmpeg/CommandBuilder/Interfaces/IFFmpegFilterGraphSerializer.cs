using Infrastructure.Engine.FFmpeg.CommandBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommandBuilder.Interfaces;

public interface IFFmpegFilterGraphSerializer
{
    string SerializeGraph(FilterGraph filterGraph);
    string SerializeLabel(string label);
}
