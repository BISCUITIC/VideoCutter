namespace Infrastructure.Configuration.Contracts;

public class PipelineStepDefinition
{
    public string Type { get; }
    public Dictionary<string, string> Parameters { get; }

    public PipelineStepDefinition(string type, Dictionary<string, string> parameters)
    {
        Type = type;
        Parameters = parameters;
    }
}
