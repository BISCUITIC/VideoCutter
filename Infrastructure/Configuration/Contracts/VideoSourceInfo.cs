namespace Infrastructure.Configuration.Contracts;

public class VideoSourceInfo
{
    public string InputFilePath { get; }
    public string OutputFolderPath { get; }

    public VideoSourceInfo(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}
