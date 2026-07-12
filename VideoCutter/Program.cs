using Application.Configuration.Interfaces;
using Application.Engine.Services.Interfaces;
using Domain.Definitions;
using FFMpegCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using VideoCutter.Extensions;
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

    private static async Task Main(string[] args)
    {
        GlobalFFOptions.Configure(options => options.BinaryFolder = BinaryFolderPath);

        ServiceCollection services = new ServiceCollection();

        Setup(services);        

        ServiceProvider provider = services.BuildServiceProvider();

        IConfigProvider configProvider = provider.GetRequiredService<IConfigProvider>();
        VideoProcessing processing = configProvider.Load(ConfigPath);

        IVideoProcessingEngine engine = provider.GetRequiredService<IVideoProcessingEngine>();

        using CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await engine.ProcessingAsync(processing, token);
    }

    public static void Setup(ServiceCollection services)
    {       
        services.AddSingleton(provider => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
        });

        services.AddJsonConfiguration();
        services.AddConsoleProgressHandler();
        services.AddFFmpegApplication();

        services.BuildProcessingEngine();
    }
}
