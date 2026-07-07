using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;
using Infrastructure.Engine.FFmpeg.Models;
using Infrastructure.Engine.FFmpeg.Services.Interfaces;

namespace Infrastructure.Engine.FFmpeg.Services;

public class FFmpegCommandBuilder : ICommandBuilder
{
    private readonly IFilterGraphBuilder _graphBuilder;
    private readonly IFilterGraphSerializer _graphSerializer;

    public FFmpegCommandBuilder(IFilterGraphBuilder graphBuilder, 
                                IFilterGraphSerializer graphSerializer)
    {
        _graphBuilder = graphBuilder;
        _graphSerializer = graphSerializer;
    }

    public Command Build(VideoSegment segment,
                         VideoProcessingDefinition definition)
    {
        FilterGraph filterGraph = _graphBuilder.Build(definition.Pipeline);

        return new Command(
            executer: "ffmpeg",
            arguments: [
                new Argument("-i", definition.Source.InputFilePath),
                new Argument("-ss", segment.Start.ToString()),
                new Argument("-to", segment.End.ToString()),
                new Argument("-filter_complex", _graphSerializer.Serialize(filterGraph)),

            ]
        );
    }
}
