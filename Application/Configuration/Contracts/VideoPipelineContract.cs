namespace Application.Configuration.Contracts;

public class VideoPipelineContract
{
    public List<PipelineStepContract> Steps { get; }

    public VideoPipelineContract(List<PipelineStepContract> steps)
    {
        Steps = steps;
    }
}
