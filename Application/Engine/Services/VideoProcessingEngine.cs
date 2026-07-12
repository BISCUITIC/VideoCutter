using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;

namespace Application.Engine.Services;

public class VideoProcessingEngine : IVideoProcessingEngine
{
    private readonly IVideoMetadataReader _metadataReader;
    private readonly IVideoSegmentor _videoSegmenter;

    private readonly ICommandBuilder _commandBuilder;
    private readonly ICommandExecutor _commandExecutor;

    private readonly IProgressHandler _progressHandler;

    private readonly SemaphoreSlim _semaphore;

    public VideoProcessingEngine(IVideoMetadataReader metadataReader,
                                 IVideoSegmentor videoSegmentor,
                                 ICommandBuilder commandBuilder,
                                 ICommandExecutor commandExecutor,
                                 IProgressHandler progressHandler)
    {
        _metadataReader = metadataReader;
        _videoSegmenter = videoSegmentor;

        _commandBuilder = commandBuilder;
        _commandExecutor = commandExecutor;

        _progressHandler = progressHandler;

        _semaphore = new SemaphoreSlim(5, 5);
    }

    public async Task ProcessingAsync(VideoProcessing definition, 
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

        _progressHandler.Start(segments.Count);

        await Task.WhenAll(processingTasks);

        _progressHandler.Finish();
    }

    private async Task ProcessSegmentAsync(int index,
                                           VideoSegment segment,
                                           VideoProcessing definition,
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
                segment,
                percentage => _progressHandler.Handle(index, percentage),
                cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
