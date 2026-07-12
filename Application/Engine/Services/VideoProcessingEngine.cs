using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services;

public class VideoProcessingEngine : IVideoProcessingEngine
{
    private readonly IVideoMetadataReader _metadataReader;
    private readonly IVideoSegmenter _videoSegmenter;

    private readonly ICommandBuilder _commandBuilder;
    private readonly ICommandExecutor _commandExecutor;

    private readonly SemaphoreSlim _semaphore;

    public VideoProcessingEngine(IVideoMetadataReader metadataReader,
                                 IVideoSegmenter videoSegmentor,
                                 ICommandBuilder commandBuilder,
                                 ICommandExecutor commandExecutor)
    {
        _metadataReader = metadataReader;
        _videoSegmenter = videoSegmentor;

        _commandBuilder = commandBuilder;
        _commandExecutor = commandExecutor;

        _semaphore = new SemaphoreSlim(5, 5);
    }

    public async Task ProcessingAsync(VideoProcessingDefinition definition, 
                                      CancellationToken cancellationToken = default)
    {
        VideoMetadata metadata = 
            await _metadataReader.ReadAsync(definition.Source.InputFilePath, cancellationToken);

        IReadOnlyCollection<VideoSegment> segments = 
            _videoSegmenter.Process(definition.Segmentation, metadata);

        IEnumerable<Task> processingTasks = 
            segments.Select(
                (segment, index) => 
                    ProcessSegmentAsync(
                        index, segment, definition, cancellationToken
                    )
            );

        await Task.WhenAll(processingTasks);
    }

    private async Task ProcessSegmentAsync(int index,
                                           VideoSegment segment,
                                           VideoProcessingDefinition definition,
                                           CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            Command command = _commandBuilder.Build(
                index,
                segment,
                definition);

            await _commandExecutor.ExecuteAsync(
                command,
                cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
