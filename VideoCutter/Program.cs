using Config.Contracts;
using Config.Services;
using Core.Models;
using Core.Services;
using Core.Services.ServicesFactories;
using FFMpegCore;
using Infrastructure.Configuration.Services;
using Infrastructure.Configuration.Services.Facade;
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

    private static void Main(string[] args)
    {
        ServiceCollection services = new ServiceCollection();

        services.AddSingleton(provider => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
        });

        services.AddSingleton<ConfigJsonParser>();
        services.AddSingleton<ConfigFileReader>();
        services.AddSingleton<ConfigProvider>();

        ServiceProvider provider = services.BuildServiceProvider();

        ConfigProvider configProvider = provider.GetRequiredService<ConfigProvider>();
        Infrastructure.Configuration.Contracts.Config config = configProvider.Load("config.json");
    }

    private static async Task PriviousVersion()
    {
        GlobalFFOptions.Configure(options => options.BinaryFolder = BinaryFolderPath);

        ConfigHandler configHandler = new ConfigHandler(ConfigPath);
        ConfigPipline config = configHandler.Load();

        IEnumerable<PipelineSegmentDefinition> segmentsDefinition = config.PipeLine.ToPipelineSegmentsDefinition();
        Pipeline pipeline = new PipelineFactory().Create(segmentsDefinition);

        TimeSpan videoDuration = FFProbe.Analyse(config.Info.InputFilePath).Duration;

        CutServiceFactory cutServiceFactory = new CutServiceFactory(config.CutService.ToCutServiceDefinition(), videoDuration);
        VideoHandlerFactory videoHandlerFactory = new VideoHandlerFactory(pipeline, config.Info.ToSessionInfo(), cutServiceFactory);
        VideoHandler videoHandler = videoHandlerFactory.Create();

        await videoHandler.Process();
    }
}
