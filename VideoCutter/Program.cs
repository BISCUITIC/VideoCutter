using Application.Configuration.Interfaces;
using Application.Configuration.Services;
using Application.Engine.Services.Interfaces;
using Domain.Commands;
using Domain.Definitions;
using Domain.Processing;
using Infrastructure.Configuration.Factories;
using Infrastructure.Configuration.Factories.Interfaces;
using Infrastructure.Configuration.Json.Services;
using Infrastructure.Engine.FFmpeg.CommadnBuilder;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Interfaces;
using Infrastructure.Engine.FFmpeg.CommadnBuilder.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
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

        services.AddSingleton<IFilterFactory, FilterFactory>();

        services.AddSingleton<IConfigReader, ConfigJsonReader>();
        services.AddSingleton<IConfigParser, ConfigJsonParser>();
        services.AddSingleton<IConfigMapper, ConfigJsonMapper>();

        services.AddSingleton<IFFmpegFilterGraphBuilder, FFmpegFilterGraphBuilder>();
        services.AddSingleton<IFFmpegFilterSerializer, FFmpegFilterSerializer>();
        services.AddSingleton<IFFmpegFilterGraphSerializer, FFmpegFilterGraphSerializer>();

        services.AddSingleton<ICommandBuilder, FFmpegCommandBuilder>();

        services.AddSingleton<ConfigProvider>();

        ServiceProvider provider = services.BuildServiceProvider();

        ConfigProvider configProvider = provider.GetRequiredService<ConfigProvider>();
        VideoProcessingDefinition processing = configProvider.Load(ConfigPath);

        ICommandBuilder commandBuilder = provider.GetRequiredService<ICommandBuilder>();
        
        Command command = commandBuilder.Build(
            new VideoSegment(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 20)), 
            processing
        );

        foreach(Argument arg in command.Arguments)
            Console.Write($"{arg.Option} {arg.Value} ");        
    }
}
