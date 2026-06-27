namespace Core.Models;

public class CutServiceDefinition
{
    public Dictionary<string, string> CutServiceParams { get; }

    public CutServiceDefinition(Dictionary<string, string> cutterServiceParams)
    {
        CutServiceParams = cutterServiceParams;
    }
}
