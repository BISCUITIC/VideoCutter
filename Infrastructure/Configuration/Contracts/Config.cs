namespace Infrastructure.Configuration.Contracts;

public class Config
{
    public VideoSourceInfo Info { get; }
    public VideoSegmentation Segmentation { get; }
    public VideoPipelineDefinition PipelineDefinition { get; }

    public Config(VideoSourceInfo info, 
                  VideoSegmentation segmentation, 
                  VideoPipelineDefinition pipelineDefinition)
    {
        Info = info;
        Segmentation = segmentation;
        PipelineDefinition = pipelineDefinition;
    }
}
