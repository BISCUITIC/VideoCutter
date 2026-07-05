using Application.Configuration.Interfaces;
using Application.Configuration.Services;
using Domain.Definitions;
using Infrastructure.Configuration.Factories;
using Infrastructure.Configuration.Factories.Interfaces;
using Infrastructure.Configuration.Json.Services;
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

        services.AddSingleton<ConfigProvider>();

        ServiceProvider provider = services.BuildServiceProvider();

        ConfigProvider configProvider = provider.GetRequiredService<ConfigProvider>();
        VideoProcessingDefinition processing = configProvider.Load(ConfigPath);
    }
}
