namespace Config.Contracts;

public class ConfigPipline
{
    public ConfigInfo Info { get; }
    public ConfigCutService Cutter { get; }
    public ConfigPipelineSegment[] PipeLine { get; }

    public ConfigPipline(ConfigInfo info, ConfigCutService cutter, ConfigPipelineSegment[] pipeLine)
    {
        Info = info;
        Cutter = cutter;
        PipeLine = pipeLine;
    }
}
