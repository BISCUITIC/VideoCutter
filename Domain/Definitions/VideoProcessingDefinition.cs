using Domain.Definitions.PipelineDefinition;

namespace Domain.Definitions;

public class VideoProcessingDefinition
{
    public VideoSource Source { get; init; }
    public VideoSegmentation Segmentation { get; init; }
    public Pipeline Pipeline {  get; init; }

    public VideoProcessingDefinition(VideoSource source, VideoSegmentation segmentation, Pipeline pipeline)
    {
        Source = source;
        Segmentation = segmentation;
        Pipeline = pipeline;
    }
}
