using Domain.Definitions;
using Domain.Filters.Interfaces;
using Infrastructure.Engine.FFmpeg.CommandBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommandBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommandBuilder.Services;

public class FFmpegFilterGraphBuilder : IFFmpegFilterGraphBuilder
{
    public FilterGraph Build(Pipeline pipeline)
    {
        List<FilterNode> nodes = new List<FilterNode>();

        int currentVideoIndex = 0;

        string inputVideoLabel = "0:v";
        string outputVideoLabel;

        foreach (IFilter filter in pipeline.Filters)
        {
            outputVideoLabel = $"video_{++currentVideoIndex}";

            nodes.Add(new FilterNode(
                filter,
                inputVideoLabel,
                outputVideoLabel
            ));

            inputVideoLabel = outputVideoLabel;
        }

        return new FilterGraph(nodes.AsReadOnly());
    }
}
