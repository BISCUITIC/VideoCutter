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

    private readonly SemaphoreSlim _semaphore;

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
        _outputFileName = "output_";

        _semaphore = new SemaphoreSlim(3);
    }

    public async Task Process()
    {
        List<Task<bool>> chunkTasks = new List<Task<bool>>();

        foreach (CutServiceInfo info in _cutService) 
        {
            await _semaphore.WaitAsync();

            chunkTasks.Add(ProcessChunk(info));
        }

        await Task.WhenAll(chunkTasks);
    }

    private async Task<bool> ProcessChunk(CutServiceInfo info)
    {
        try
        {
            var arguments = FFMpegArguments.FromFileInput(_inputFilePath)
                               .OutputToFile(OutputFilePath(info.Iteration),
                                             true,
                                             options =>
                                             {
                                                 _cutService.Process(options, info);
                                                 _pipeline.Apply(options);
                                             });

            Console.WriteLine(arguments.Arguments);

            bool success = await arguments.ProcessAsynchronously();

            return success;
        }
        catch (Exception ex)
        {            
            Console.WriteLine($"Error while processing chunk {info}: {ex.Message}");
            return false; 
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
