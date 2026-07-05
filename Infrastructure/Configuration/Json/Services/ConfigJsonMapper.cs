using Application.Configuration.Contracts;
using Application.Configuration.Interfaces;
using Domain.Definitions;
using Infrastructure.Configuration.Factories.Interfaces;

namespace Infrastructure.Configuration.Json.Services;

public class ConfigJsonMapper : IConfigMapper
{
    private readonly IFilterFactory _filterFactory;

    public ConfigJsonMapper(IFilterFactory filterFactory)
    {
        _filterFactory = filterFactory;
    }

    public VideoProcessingDefinition Map(ConfigContract config)
    {
        VideoSource source = MapVideoSource(config.Info);
        VideoSegmentation segmentation = MapVideoSegmentation(config.Segmentation);
        Pipeline pipeline = MapPipeline(config.PipelineDefinition);

        return new VideoProcessingDefinition(source, segmentation, pipeline);
    }

    private VideoSource MapVideoSource(VideoSourceContract contract)
    {
        return new VideoSource(contract.InputFilePath,
                               contract.OutputFolderPath);
    }

    private VideoSegmentation MapVideoSegmentation(VideoSegmentationContract contract)
    {
        return new VideoSegmentation(contract.ChunkDuration,
                                     contract.Offset ?? TimeSpan.Zero,
                                     contract.MaxChunks);
    }

    private Pipeline MapPipeline(VideoPipelineContract contract)
    {
        var filters = contract.Steps
                              .Select(step => 
                                  _filterFactory.Create(step.Type, step.Parameters)
                              )
                              .ToList()
                              .AsReadOnly();

        return new Pipeline(filters);
    }
}
