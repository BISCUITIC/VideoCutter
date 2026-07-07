using Infrastructure.Engine.FFmpeg.Models;

namespace Infrastructure.Engine.FFmpeg.Interfaces;

public interface IFFmpegFilterGraphSerializer
{
    string SerializeGraph(FilterGraph filterGraph);
    string SerializeLabel(string label);
}
