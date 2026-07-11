using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services;

public class VideoProcessingEngine : IVideoProcessingEngine
{
    private readonly IVideoMetadataReader _metadataReader;
    private readonly ICommandBuilder _commandBuilder;
    private readonly ICommandExecutor _commandExecutor;

    private readonly VideoSegmenter _videoSegmenter;

    public VideoProcessingEngine(IVideoMetadataReader metadataReader,
                                 ICommandBuilder commandBuilder,
                                 ICommandExecutor commandExecutor,
                                 VideoSegmenter videoSegmentor)
    {
        _metadataReader = metadataReader;
        _commandBuilder = commandBuilder;
        _commandExecutor = commandExecutor;

        _videoSegmenter = videoSegmentor;
    }

    public async Task ProcessingAsync(VideoProcessingDefinition definition, 
                                      CancellationToken cancellationToken = default)
    {
        VideoMetadata metadata = 
            await _metadataReader.ReadAsync(definition.Source.InputFilePath, cancellationToken);

        IReadOnlyCollection<VideoSegment> segments = 
            _videoSegmenter.Process(definition.Segmentation, metadata);

        int index = 0;

        foreach (VideoSegment segment in segments)
        {
            Command command = _commandBuilder.Build(index ,segment, definition);

            await _commandExecutor.ExecuteAsync(command, cancellationToken);

            index++;
        }        
    }
}
