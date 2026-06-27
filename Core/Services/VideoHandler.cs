using Core.Models;
using FFMpegCore;

namespace Core.Services;

public class VideoHandler
{
    private readonly CutService _cutService;
    private readonly string _inputFilePath;
    private readonly string _outputFolderPath;
    private readonly string _outputFileName;

    private string OutputFilePath(int iteration)
    {
       return _outputFolderPath + _outputFileName + iteration + ".mp4";
    }

    public VideoHandler(CutService cutService, SessionInfo sessionInfo)
    {
        _cutService = cutService;
        _inputFilePath = sessionInfo.InputFilePath;
        _outputFolderPath = sessionInfo.OutputFolderPath;
        _outputFileName = "outPut";
    }

    public void Process()
    {
        while (_cutService.CanMoveNext())
        {
            FFMpegArguments.FromFileInput(_inputFilePath)
                           .OutputToFile(OutputFilePath(_cutService.CurrentIteration),
                                         true,
                                         options => _cutService.Process(options))
                           .ProcessAsynchronously();

            _cutService.MoveNext();
        }
    }
}
