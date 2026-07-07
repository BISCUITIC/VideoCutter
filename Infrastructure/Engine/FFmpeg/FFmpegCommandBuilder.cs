using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;
using Infrastructure.Engine.FFmpeg.Interfaces;
using Infrastructure.Engine.FFmpeg.Models;

namespace Infrastructure.Engine.FFmpeg;

public class FFmpegCommandBuilder : ICommandBuilder
{
    private readonly IFFmpegFilterGraphBuilder _graphBuilder;
    private readonly IFFmpegFilterGraphSerializer _graphSerializer;

    public FFmpegCommandBuilder(IFFmpegFilterGraphBuilder graphBuilder, 
                                IFFmpegFilterGraphSerializer graphSerializer)
    {
        _graphBuilder = graphBuilder;
        _graphSerializer = graphSerializer;
    }

    public Command Build(VideoSegment segment,
                         VideoProcessingDefinition definition)
    {
        FilterGraph filterGraph = _graphBuilder.Build(definition.Pipeline);

        var arguments = new List<Argument>
        {
            new Argument("-ss", segment.Start.ToString()),
            new Argument("-to", segment.End.ToString()),
            new Argument("-i", definition.Source.InputFilePath)
        };

        if (!filterGraph.IsEmpty)
        {
            arguments.Add(
                new Argument(
                    "-filter_complex", 
                    _graphSerializer.SerializeGraph(filterGraph)
                )
            );
            arguments.Add(
                new Argument(
                    "-map", 
                    _graphSerializer.SerializeLabel(filterGraph.OutputVideoLabel!)
                )
            );
        }
        else
        {
            arguments.Add(new Argument("-map", "0:v?"));
        }

        return new Command("ffmpeg", arguments);
    }
}
