namespace Application.Configuration.Contracts;

public class ConfigContract
{
    public VideoSourceContract Info { get; }
    public VideoSegmentationContract Segmentation { get; }
    public VideoPipelineContract PipelineDefinition { get; }

    public ConfigContract(VideoSourceContract info, 
                  VideoSegmentationContract segmentation, 
                  VideoPipelineContract pipelineDefinition)
    {
        Info = info;
        Segmentation = segmentation;
        PipelineDefinition = pipelineDefinition;
    }
}
