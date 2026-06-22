using System.Text.Json.Serialization;

namespace Config.Contracts;

public class ConfigData
{    
    public ConfigInfo Info { get; }
    public ConfigPipelineSegment[] PipeLine { get; }
    
    public ConfigData(ConfigInfo info, ConfigPipelineSegment[] pipeLine)
    {
        Info = info;
        PipeLine = pipeLine;
    }
}
