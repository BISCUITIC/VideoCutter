using Config.Contracts;
using Config.Services;
using Core;
using Core.Services;
using Core.Services.Models;
using FFMpegCore;

namespace VideoCutter;

internal class Program
{
    private static readonly string BinaryFolderPath = Path.Combine(AppContext.BaseDirectory,
                                                           "Tools",
                                                           "ffmpeg-8.1.1-full_build",
                                                           "ffmpeg-8.1.1-full_build",
                                                           "bin");

    private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory,
                                                           "config.json");

    private static readonly string VideoInputFilePath = Path.Combine(AppContext.BaseDirectory,
                                                        "Test",
                                                        "input.mp4");
    private static readonly string VideoOutputFilePath = Path.Combine(AppContext.BaseDirectory,
                                                              "Test",
                                                              "output_blur_2.mp4");

    private static void Main(string[] args)
    {
        GlobalFFOptions.Configure(options => options.BinaryFolder = BinaryFolderPath);

        ConfigHandler configHandler = new ConfigHandler(ConfigPath);
        PipelineConfig config = configHandler.Load();

        IEnumerable<PipelineSegmentDefinition> segmentDefinitions =
            config.PipeLine.Select(item => new PipelineSegmentDefinition(item.Type, item.SegmentParams));

        PipelineFactory factory = new PipelineFactory();
        Pipeline pipeline = factory.Create(segmentDefinitions);

        FFMpegArguments.FromFileInput(VideoInputFilePath)
                       .OutputToFile(VideoOutputFilePath,
                                     true,
                                     options => pipeline.Execute(options))
                       .ProcessAsynchronously();
    }
}
