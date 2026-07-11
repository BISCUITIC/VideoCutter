using Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommadnBuilder.Services;

public class FFmpegFilterGraphSerializer : IFFmpegFilterGraphSerializer
{
    private readonly IFFmpegFilterSerializer _filterSerializer;

    public FFmpegFilterGraphSerializer(IFFmpegFilterSerializer filterSerializer)
    {
        _filterSerializer = filterSerializer;
    }

    public string SerializeGraph(FilterGraph filterGraph)
    {
        return string.Join(
            ";",
            filterGraph.Nodes.Select(node =>
                $"{SerializeLabel(node.InputLabel)}" +
                _filterSerializer.Serialize(node.Filter) +
                $"{SerializeLabel(node.OutputLabel)}"
            )
        );
    }

    public string SerializeLabel(string label)
    {
        return $"[{label}]";
    }
}
