using Core.Segments.Interfaces;
using FFMpegCore;

namespace Core.Services;

public class Pipeline
{
    private readonly List<IPiplineSegment> _segments;
    private readonly FilterGraphBuilder _graph;

    public Pipeline(List<IPiplineSegment> segments)
    {
        _segments = segments;
        _graph = new FilterGraphBuilder();
    }

    public void Apply(FFMpegArgumentOptions options)
    {
        _graph.Init();

        foreach (var segment in _segments)
        {
            _graph.Add(segment.Apply());
        }

        options.WithCustomArgument(_graph.Build());
    }
}
