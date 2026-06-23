using Config.Services;
using Core.Services;
using Core.Services.Models;

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
                                                              "output.mp4");

    private static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //GlobalFFOptions.Configure(options => options.BinaryFolder = BinaryFolderPath);

        //FFMpegArguments.FromFileInput(VideoInputFilePath)
        //               .OutputToFile(VideoOutputFilePath, 
        //                             true, 
        //                             options => options.Seek(TimeSpan.FromSeconds(10))
        //                                               .WithDuration(TimeSpan.FromSeconds(20))
        //                                               .CopyChannel())
        //               .ProcessAsynchronously();

        ConfigHandler configHandler = new ConfigHandler(ConfigPath);


        //Console.WriteLine(Path.Exists(ConfigPath));
        //string output;
        //configHandler.Load().PipeLine[0].SegmentParams.TryGetValue("start", out output);
        //Console.WriteLine(output);

        PipelineFactory factory = new PipelineFactory();
        factory.Create(configHandler.Load()
                                    .PipeLine
                                    .Select(item => new PipelineSegmentDefinition(item.Type, item.SegmentParams)));
    }
}
