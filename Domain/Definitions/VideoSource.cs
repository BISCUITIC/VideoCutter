namespace Domain.Definitions;

public class VideoSource
{
    public string InputFilePath { get; init; }
    public string OutputFolderPath { get; init; }

    public VideoSource(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}

