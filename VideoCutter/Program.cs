using Application.Configuration.Interfaces;
using Application.Configuration.Services;
using Application.Engine.Services;
using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;
using FFMpegCore;
using Infrastructure.Configuration.Factories;
using Infrastructure.Configuration.Factories.Interfaces;
using Infrastructure.Configuration.Json.Services;
using Infrastructure.Engine.Common.Interfaces;
using Infrastructure.Engine.Common.Services;
using Infrastructure.Engine.FFmpeg.CommadnBuilder;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Services;
using Infrastructure.Engine.FFmpeg.CommandExecuter;
using Infrastructure.Engine.FFmpeg.VideoMetadataReader;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Text.Json;
using VideoCutter.Progress;
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

        services.AddSingleton(provider => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyProperties = false,
        });

        services.AddSingleton<IFilterFactory, FilterFactory>();

        services.AddSingleton<IConfigReader, ConfigJsonReader>();
        services.AddSingleton<IConfigParser, ConfigJsonParser>();
        services.AddSingleton<IConfigMapper, ConfigJsonMapper>();

        services.AddSingleton<IOutputPathProvider, OutputPathProvider>();
        services.AddSingleton<IFFmpegFilterGraphBuilder, FFmpegFilterGraphBuilder>();
        services.AddSingleton<IFFmpegFilterSerializer, FFmpegFilterSerializer>();
        services.AddSingleton<IFFmpegFilterGraphSerializer, FFmpegFilterGraphSerializer>();

        services.AddSingleton<IVideoMetadataReader, FFmpegVideoMetadataReader>();
        services.AddSingleton<IVideoSegmenter, VideoSegmenter>();
        services.AddSingleton<ICommandBuilder, FFmpegCommandBuilder>();
        services.AddSingleton<ICommandExecutor, FFmpegCommandExecuter>();
        services.AddSingleton<IProgressHandler, ConsoleProgressHandler>();
        services.AddSingleton<IVideoProcessingEngine, VideoProcessingEngine>();

        services.AddSingleton<ConfigProvider>();

        ServiceProvider provider = services.BuildServiceProvider();

        ConfigProvider configProvider = provider.GetRequiredService<ConfigProvider>();
        VideoProcessingDefinition processing = configProvider.Load(ConfigPath);

        IVideoProcessingEngine engine = provider.GetRequiredService<IVideoProcessingEngine>();

        using CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await engine.ProcessingAsync(processing, token);
        
    }
}
