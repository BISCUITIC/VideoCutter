namespace Domain.Definitions.PipelineDefinition;

public class Segment
{
    public SegmentType Type { get; init; }
    public IReadOnlyDictionary<string, string> Parameters { get; init; }

    public Segment(SegmentType type, IReadOnlyDictionary<string, string> parameters)
    {
        Type = type;
        Parameters = parameters;
    }
}
