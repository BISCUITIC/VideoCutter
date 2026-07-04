namespace Domain.Definitions.PipelineDefinition;

public class Pipeline
{
    public IReadOnlyCollection<Segment> Segments { get; init; }

    public Pipeline(IReadOnlyCollection<Segment> segments)
    {
        Segments = segments;
    }
}
