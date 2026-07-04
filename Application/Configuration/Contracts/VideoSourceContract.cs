namespace Application.Configuration.Contracts;

public class VideoSourceContract
{
    public string InputFilePath { get; }
    public string OutputFolderPath { get; }

    public VideoSourceContract(string inputFilePath, string outputFolderPath)
    {
        InputFilePath = inputFilePath;
        OutputFolderPath = outputFolderPath;
    }
}
