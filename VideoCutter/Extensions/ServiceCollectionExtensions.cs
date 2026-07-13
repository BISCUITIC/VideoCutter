using Application.Configuration.Interfaces;
using Application.Configuration.Services;
using Application.Engine.Services;
using Application.Engine.Services.Interfaces;
using Infrastructure.Configuration.Factories;
using Infrastructure.Configuration.Factories.Interfaces;
using Infrastructure.Configuration.Json.Services;
using Infrastructure.Engine.Common.Interfaces;
using Infrastructure.Engine.Common.Services;
using Infrastructure.Engine.Fake;
using Infrastructure.Engine.FFmpeg.CommandBuilder;
using Infrastructure.Engine.FFmpeg.CommandBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommandBuilder.Services;
using Infrastructure.Engine.FFmpeg.CommandExecuter;
using Infrastructure.Engine.FFmpeg.VideoMetadataReader;
using Microsoft.Extensions.DependencyInjection;
using VideoCutter.Progress;
using VideoCutter.Progress.Interfaces;

namespace VideoCutter.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddJsonConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IFilterFactory, FilterFactory>();

        services.AddSingleton<IConfigReader, ConfigJsonReader>();
        services.AddSingleton<IConfigParser, ConfigJsonParser>();
        services.AddSingleton<IConfigMapper, ConfigJsonMapper>();

        services.AddSingleton<IConfigProvider, ConfigProvider>();
    }

    public static void AddConsoleProgressHandler(this IServiceCollection services)
    {
        services.AddTransient<IProgressBar, ConsoleProgressBar>();
        services.AddTransient<IProcessingTimer, ConsoleTimer>();
        services.AddTransient<IProgressHandler, ConsoleProgressHandler>();
    }

    public static void AddFFmpegProcessing(this IServiceCollection services)
    {
        services.AddCommonInfrastructure();

        services.AddSingleton<IFFmpegFilterGraphBuilder, FFmpegFilterGraphBuilder>();
        services.AddSingleton<IFFmpegFilterSerializer, FFmpegFilterSerializer>();
        services.AddSingleton<IFFmpegFilterGraphSerializer, FFmpegFilterGraphSerializer>();

        services.AddSingleton<IVideoMetadataReader, FFmpegVideoMetadataReader>();
        services.AddSingleton<IVideoSegmentor, VideoSegmentor>();

        services.AddSingleton<ICommandBuilder, FFmpegCommandBuilder>();
        services.AddSingleton<ICommandExecutor, FFmpegCommandExecuter>();
    }

    public static void AddFakeProcessing(this IServiceCollection services)
    {
        services.AddCommonInfrastructure();

        services.AddSingleton<IFFmpegFilterGraphBuilder, FFmpegFilterGraphBuilder>();
        services.AddSingleton<IFFmpegFilterSerializer, FFmpegFilterSerializer>();
        services.AddSingleton<IFFmpegFilterGraphSerializer, FFmpegFilterGraphSerializer>();

        services.AddSingleton<IVideoMetadataReader, FFmpegVideoMetadataReader>();
        services.AddSingleton<IVideoSegmentor, VideoSegmentor>();

        services.AddSingleton<ICommandBuilder, FFmpegCommandBuilder>();
        services.AddSingleton<ICommandExecutor, FakeCommandExecutor>();
    }

    public static void AddProcessingEngine(this IServiceCollection services)
    {
        services.AddTransient<IVideoProcessingEngine, VideoProcessingEngine>();
    }

    private static void AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IOutputPathProvider, OutputPathProvider>();
    }
}
