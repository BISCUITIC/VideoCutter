using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services;

public class VideoProcessingEngine : IVideoProcessingEngine
{
    private readonly IVideoSegmenter _videoSegmenter;
    private readonly ICommandBuilder _commandBuilder;
    private readonly ICommandExecutor _commandExecutor;

    public VideoProcessingEngine(IVideoSegmenter videoSegmentor,
                                 ICommandBuilder commandBuilder,
                                 ICommandExecutor commandExecutor)
    {
        _videoSegmenter = videoSegmentor;
        _commandBuilder = commandBuilder;
        _commandExecutor = commandExecutor;
    }

    public async Task ProcessingAsync(VideoProcessingDefinition definition, 
                                      CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<VideoSegment> segments = 
            _videoSegmenter.Process(definition.Segmentation);

        foreach (VideoSegment segment in segments)
        {
            Command command = _commandBuilder.Build(segment, definition);

            await _commandExecutor.ExecuteAsync(command, cancellationToken);
        }        
    }
}
