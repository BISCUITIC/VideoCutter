using Core.Segments;
using Core.Segments.Interfaces;
using Core.Services.SegmentsFactories.Attributes;
using Core.Services.SegmentsFactories.Interfaces;

namespace Core.Services.SegmentsFactories;

[SegmentFactory("Cut")]
public class CutSegmentFactory : ISegmentFactory
{
    public CutSegmentFactory() { }

    public IPiplineSegment Create(Dictionary<string, string> segmentParams)
    {
        TimeSpan startTime = TimeSpan.Parse(segmentParams["start"]);

        TimeSpan endTime = TimeSpan.Parse(segmentParams["end"]);

        Console.WriteLine("Factory");

        return new CutSegment(startTime, endTime);
    }
}
