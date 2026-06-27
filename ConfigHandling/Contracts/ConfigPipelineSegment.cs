namespace Config.Contracts;

public class ConfigPipelineSegment
{
    public string Type { get; }
    public Dictionary<string, string> SegmentParams { get; }

    public ConfigPipelineSegment(string type, Dictionary<string, string> segmentParams)
    {
        Type = type;
        SegmentParams = segmentParams;
    }
}
