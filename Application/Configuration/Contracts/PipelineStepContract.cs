namespace Application.Configuration.Contracts;

public class PipelineStepContract
{
    public string Type { get; }
    public Dictionary<string, string> Parameters { get; }

    public PipelineStepContract(string type, Dictionary<string, string> parameters)
    {
        Type = type;
        Parameters = parameters;
    }
}
