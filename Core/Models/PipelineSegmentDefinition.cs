namespace Core.Models;

public sealed class PipelineSegmentDefinition
{
    public string Type { get; }
    public Dictionary<string, string> SegmentParams { get; }

    public PipelineSegmentDefinition(string type, Dictionary<string, string> segmentParams)
    {
        Type = type;
        SegmentParams = segmentParams;
    }
}
