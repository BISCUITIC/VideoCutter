namespace Config.Contracts;

public class ConfigCutService
{
    public Dictionary<string, string> CutServiceParams { get; }

    public ConfigCutService(Dictionary<string, string> cutterServiceParams)
    {
        CutServiceParams = cutterServiceParams;
    }
}
