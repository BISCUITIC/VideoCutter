using Core.Segments.Interfaces;
using FFMpegCore;
using System.Text;

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
        StringBuilder filter = new StringBuilder();

        filter.Append("-filter_complex \"");

        foreach (var segment in _segments)
        {
            segment.Apply(options, filter);
        }
        
        filter.Append("\"");

        options.WithCustomArgument(filter.ToString());
    }
}
