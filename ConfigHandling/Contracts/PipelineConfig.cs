namespace Config.Contracts;

public class PipelineConfig
{
    public ConfigInfo Info { get; }
    public PipelineSegment[] PipeLine { get; }

    public PipelineConfig(ConfigInfo info, PipelineSegment[] pipeLine)
    {
        Info = info;
        PipeLine = pipeLine;
    }
}
