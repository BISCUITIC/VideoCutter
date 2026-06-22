using FFMpegCore;
using Config;

namespace VideoCutter;

internal class Program
{    
    static readonly string BinaryFolderPath = Path.Combine(AppContext.BaseDirectory,
                                                           "Tools", 
                                                           "ffmpeg-8.1.1-full_build", 
                                                           "ffmpeg-8.1.1-full_build", 
                                                           "bin");

    static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory,
                                                           "config.json");

    static readonly string VideoInputFilePath = Path.Combine(AppContext.BaseDirectory, 
                                                        "Test", 
                                                        "input.mp4");
    static readonly string VideoOutputFilePath = Path.Combine(AppContext.BaseDirectory, 
                                                              "Test", 
                                                              "output.mp4");

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        GlobalFFOptions.Configure(options => options.BinaryFolder = BinaryFolderPath);

        FFMpegArguments.FromFileInput(VideoInputFilePath)
                       .OutputToFile(VideoOutputFilePath, 
                                     true, 
                                     options => options.Seek(TimeSpan.FromSeconds(10))
                                                       .WithDuration(TimeSpan.FromSeconds(20))
                                                       .CopyChannel())
                       .ProcessAsynchronously();

        ConfigHandler configHandler = new ConfigHandler(ConfigPath);


        Console.WriteLine(Path.Exists(ConfigPath));
        string output;
        configHandler.Handle().PipeLine[0].SegmentParams.TryGetValue("start", out output);
        Console.WriteLine(output);
    }
}
