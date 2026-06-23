namespace Config.Contracts;

public class PipelineSegment
{
    public string Type { get; }
    public Dictionary<string, string> SegmentParams { get; }

    public PipelineSegment(string type, Dictionary<string, string> segmentParams)
    {
        Type = type;
        SegmentParams = segmentParams;
    }
}
