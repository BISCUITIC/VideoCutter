namespace Config.Contracts;

public class SessionInfo
{
    public string InputFilePath { get; }
    public string OutputFolderPath { get; }

    public SessionInfo(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}
