namespace Domain.Definitions;

public class VideoProcessing
{
    public VideoSource Source { get; }
    public VideoSegmentation Segmentation { get; }
    public Pipeline Pipeline {  get; }

    public VideoProcessing(VideoSource source, VideoSegmentation segmentation, Pipeline pipeline)
    {
        Source = source;
        Segmentation = segmentation;
        Pipeline = pipeline;
    }
}
