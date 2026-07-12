using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;
using Infrastructure.Engine.Common.Interfaces;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Models;

namespace Infrastructure.Engine.FFmpeg.CommadnBuilder;

public class FFmpegCommandBuilder : ICommandBuilder
{
    private readonly IFFmpegFilterGraphBuilder _graphBuilder;
    private readonly IFFmpegFilterGraphSerializer _graphSerializer;
    private readonly IOutputPathProvider _outputPathProvider;

    public FFmpegCommandBuilder(IFFmpegFilterGraphBuilder graphBuilder,
                                IFFmpegFilterGraphSerializer graphSerializer,
                                IOutputPathProvider outputPathProvider)
    {
        _graphBuilder = graphBuilder;
        _graphSerializer = graphSerializer;
        _outputPathProvider = outputPathProvider;
    }

    public Command Build(int index,
                         VideoSegment segment,
                         VideoProcessing definition)
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

        arguments.Add(new Argument("-map", "0:a?"));

        arguments.Add(
            new Argument(
                null,
                _outputPathProvider.GetOutputPath(index, definition, segment)
            )
        );

        return new Command("ffmpeg", arguments);
    }
}
