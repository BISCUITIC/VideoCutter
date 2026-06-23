using Core.Segments.Interfaces;

namespace Core.Services.SegmentsFactories.Interfaces;

public interface ISegmentFactory
{
    IPiplineSegment Create(Dictionary<string, string> segmentParams);
}
