namespace Config.Contracts;

public class PipelineConfig
{
    public SessionInfo Info { get; }
    public PipelineSegment[] PipeLine { get; }

    public PipelineConfig(SessionInfo info, PipelineSegment[] pipeLine)
    {
        Info = info;
        PipeLine = pipeLine;
    }
}
