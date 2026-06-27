using Core.Models;
using FFMpegCore;

namespace Core.Services;

public class VideoHandler
{
    private readonly Pipeline _pipeline;
    private readonly CutService _cutService;
    private readonly string _inputFilePath;
    private readonly string _outputFolderPath;
    private readonly string _outputFileName;

    private string OutputFilePath(int iteration)
    {
        return Path.Combine(_outputFolderPath, _outputFileName + iteration.ToString() + ".mp4");
    }

    public VideoHandler(Pipeline pipeline, CutService cutService, SessionInfo sessionInfo)
    {
        _pipeline = pipeline;
        _cutService = cutService;

        _inputFilePath = sessionInfo.InputFilePath;
        _outputFolderPath = sessionInfo.OutputFolderPath;
        _outputFileName = "outPut";
    }

    public void Process()
    {
        while (_cutService.CanMoveNext())
        {
            var arguments = FFMpegArguments.FromFileInput(_inputFilePath)
                           .OutputToFile(OutputFilePath(_cutService.CurrentIteration),
                                         true,
                                         options =>
                                         {
                                             _cutService.Process(options);
                                             _pipeline.Apply(options);
                                         });

            Console.WriteLine(arguments.Arguments);

            arguments.ProcessSynchronously();

            _cutService.MoveNext();
        }
    }
}
