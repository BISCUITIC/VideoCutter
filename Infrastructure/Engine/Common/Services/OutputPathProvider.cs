using Domain.Definitions;
using Domain.Processing;
using Infrastructure.Engine.Common.Interfaces;

namespace Infrastructure.Engine.Common.Services;

public class OutputPathProvider : IOutputPathProvider
{ 
    public string GetOutputPath(int segmentNumber,
                                VideoProcessing definition,
                                VideoSegment segment)
    {

        string outputFolderPath =
            definition.Source.OutputFolderPath;

        Directory.CreateDirectory(outputFolderPath);

        string sourceFileName = Path.GetFileNameWithoutExtension(
            definition.Source.InputFilePath);

        string sourceExtension = ".mp4";

        string outputFileName =
            $"{sourceFileName}_segment_{segmentNumber:D3}" +
            $"{sourceExtension}";

        return Path.Combine(outputFolderPath, outputFileName);
    }
}
