using Application.Configuration.Interfaces;
using Application.Configuration.Services;
using Application.Engine.Services;
using Application.Engine.Services.Interfaces;
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
using VideoCutter.Progress;

namespace VideoCutter.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddJsonConfiguration(this ServiceCollection services)
    {
        services.AddSingleton<IFilterFactory, FilterFactory>();

        services.AddSingleton<IConfigReader, ConfigJsonReader>();
        services.AddSingleton<IConfigParser, ConfigJsonParser>();
        services.AddSingleton<IConfigMapper, ConfigJsonMapper>();

        services.AddSingleton<IConfigProvider, ConfigProvider>();
    }

    public static void AddFFmpegApplication(this ServiceCollection services)
    {
        services.AddFFmpegInfrastructure();

        services.AddSingleton<IVideoMetadataReader, FFmpegVideoMetadataReader>();
        services.AddSingleton<IVideoSegmenter, VideoSegmenter>();

        services.AddSingleton<ICommandBuilder, FFmpegCommandBuilder>();
        services.AddSingleton<ICommandExecutor, FFmpegCommandExecuter>();

        services.AddSingleton<IProgressHandler, ConsoleProgressHandler>();

        services.AddSingleton<IVideoProcessingEngine, VideoProcessingEngine>();
    }

    public static void AddFFmpegInfrastructure(this ServiceCollection services)
    {
        services.AddCommonInfrastructure();

        services.AddSingleton<IFFmpegFilterGraphBuilder, FFmpegFilterGraphBuilder>();
        services.AddSingleton<IFFmpegFilterSerializer, FFmpegFilterSerializer>();
        services.AddSingleton<IFFmpegFilterGraphSerializer, FFmpegFilterGraphSerializer>();
    }

    public static void AddCommonInfrastructure(this ServiceCollection services)
    {
        services.AddSingleton<IOutputPathProvider, OutputPathProvider>();
    }
}
