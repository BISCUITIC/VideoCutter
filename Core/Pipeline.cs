using Core.Segments.Interfaces;
using FFMpegCore;

namespace Core;

public class Pipeline
{
    private readonly List<IPiplineSegment> _segments;

    public Pipeline(List<IPiplineSegment> segments)
    {
        _segments = segments;
    }

    public void Execute(FFMpegArgumentOptions options)
    {
        foreach (var segment in _segments)
        {
            segment.Apply(options);
        }        
    }
}
