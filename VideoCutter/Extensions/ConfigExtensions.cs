using Config.Contracts;
using Core.Models;
namespace VideoCutter.Extensions;

internal static class ConfigExtensions
{
    public static PipelineSegmentDefinition ToPipelineSegmentDefinition(
        this ConfigPipelineSegment pipelineSegment
    )
    {
        return new PipelineSegmentDefinition(pipelineSegment.Type, pipelineSegment.SegmentParams);
    }

    public static SessionInfo ToSessionInfo(this ConfigInfo configInfo)
    {
        return new SessionInfo(configInfo.InputFilePath, configInfo.OutputFolderPath);
    }

    public static CutServiceDefinition ToCutServiceDefinition(this ConfigCutService configCutService)
    {
        return new CutServiceDefinition(configCutService.CutServiceParams);
    }

    public static IEnumerable<PipelineSegmentDefinition> ToPipelineSegmentsDefinition(
        this IEnumerable<ConfigPipelineSegment> pipelineSegments
    )
    {
        return pipelineSegments.Select(ToPipelineSegmentDefinition);
    }
}
