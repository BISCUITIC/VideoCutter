namespace Domain.Definitions;

public class VideoProcessingDefinition
{
    public VideoSource Source { get; }
    public VideoSegmentation Segmentation { get; }
    public Pipeline Pipeline {  get; }

    public VideoProcessingDefinition(VideoSource source, VideoSegmentation segmentation, Pipeline pipeline)
    {
        Source = source;
        Segmentation = segmentation;
        Pipeline = pipeline;
    }
}
