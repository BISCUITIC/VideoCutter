using Infrastructure.Engine.FFmpeg.CommadnBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;

public interface IFFmpegFilterGraphSerializer
{
    string SerializeGraph(FilterGraph filterGraph);
    string SerializeLabel(string label);
}
