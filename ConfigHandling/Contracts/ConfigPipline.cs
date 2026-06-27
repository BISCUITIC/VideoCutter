namespace Config.Contracts;

public class ConfigPipline
{
    public ConfigInfo Info { get; }
    public ConfigCutService CutService { get; }
    public ConfigPipelineSegment[] PipeLine { get; }

    public ConfigPipline(ConfigInfo info, 
                         ConfigCutService cutService, 
                         ConfigPipelineSegment[] pipeLine)
    {
        Info = info;
        CutService = cutService;
        PipeLine = pipeLine;
    }
}
