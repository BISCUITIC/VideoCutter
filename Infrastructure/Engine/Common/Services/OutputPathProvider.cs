using Domain.Definitions;
using Domain.Processing;

namespace Infrastructure.Engine.Common.Services;

public class OutputPathProvider
{
    public string GetOutputPath(VideoProcessingDefinition definition,
                                VideoSegment segment,
                                int segmentNumber)
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
