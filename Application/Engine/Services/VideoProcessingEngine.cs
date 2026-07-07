using Application.Engine.Services.Interfaces;
using Domain.Definitions;
using Domain.Engine;

namespace Application.Engine.Services;

public class VideoProcessingEngine : IVideoProcessingEngine
{
    private readonly IVideoSegmentor _videoSegmentor;
    private readonly ICommandBuilder _commandBuilder;
    private readonly ICommandExecutor _commandExecutor;

    public VideoProcessingEngine(IVideoSegmentor videoSegmentor,
                                 ICommandBuilder commandBuilder,
                                 ICommandExecutor commandExecutor)
    {
        _videoSegmentor = videoSegmentor;
        _commandBuilder = commandBuilder;
        _commandExecutor = commandExecutor;
    }

    public async Task ProcessingAsync(VideoProcessingDefinition definition, 
                                      CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<VideoSegment> segments = 
            _videoSegmentor.Process(definition.Segmentation);

        foreach (VideoSegment segment in segments)
        {
            Command command = _commandBuilder.Build(segment, definition);

            await _commandExecutor.ExecuteAsync(command, cancellationToken);
        }        
    }
}
