namespace Config.Contracts;

public class ConfigInfo
{
    public string InputFilePath { get; }
    public string OutputFolderPath { get; }

    public ConfigInfo(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}
