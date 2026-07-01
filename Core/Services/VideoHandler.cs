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

    private readonly ProgressTracker _progressTracker;  
    private readonly ConsoleProgressRenderer _progressRenderer;

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

        Console.WriteLine(_cutService.NumberIteration);
        _progressTracker = new ProgressTracker(_cutService.NumberIteration);
        _progressRenderer = new ConsoleProgressRenderer(_cutService.NumberIteration);
        _progressTracker.OnChange += _progressRenderer.Update;
    }

    public async Task Process()
    {
        var a = DateTime.Now;
        Console.WriteLine(DateTime.Now);

        List<Task<bool>> chunkTasks = new List<Task<bool>>();

        foreach (CutServiceInfo info in _cutService) 
        {
            await _semaphore.WaitAsync();

            chunkTasks.Add(ProcessChunk(info));
        }

        await Task.WhenAll(chunkTasks);
        var b = DateTime.Now;
        Console.WriteLine(b);
        Console.WriteLine(b - a);
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
                                                 options.WithCustomArgument("-threads 2");
                                             })
                               .NotifyOnProgress(progress =>
                                                {                                                    
                                                    _progressTracker.Report(info.Iteration, progress);                                                    
                                                },
                                                info.Duration
                               );                        

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
