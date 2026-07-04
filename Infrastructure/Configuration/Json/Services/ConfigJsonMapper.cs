using Application.Configuration.Contracts;
using Application.Configuration.Interfaces;
using Domain.Definitions;
using Domain.Definitions.PipelineDefinition;

namespace Infrastructure.Configuration.Json.Services;

public class ConfigJsonMapper : IConfigMapper
{
    public VideoProcessingDefinition Map(ConfigContract config)
    {
        VideoSource source = MapVideoSource(config.Info);
        VideoSegmentation segmentation = MapVideoSegmentation(config.Segmentation);
        Pipeline pipeline = MapPipeline(config.PipelineDefinition);

        return new VideoProcessingDefinition(source, segmentation,pipeline);
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

    private Pipeline MapPipeline(VideoPipelineContract contract) {
        
        IReadOnlyCollection<Segment> segments = 
            contract.Steps.Select(step => 
                {
                    Type enumType = typeof(SegmentType);
                    SegmentType type = (SegmentType)Enum.Parse(enumType, step.Type);
                    return new Segment(type, step.Parameters);
                }
            )
            .ToList()
            .AsReadOnly();

        return new Pipeline(segments);
    }
}
