namespace Infrastructure.Configuration.Contracts;

public class VideoPipelineDefinition
{
    public List<PipelineStepDefinition> Steps { get; }

    public VideoPipelineDefinition(List<PipelineStepDefinition> steps)
    {
        Steps = steps;
    }
}
