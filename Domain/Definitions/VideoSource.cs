namespace Domain.Definitions;

public class VideoSource
{
    public string InputFilePath { get; }
    public string OutputFolderPath { get; }

    public VideoSource(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}

