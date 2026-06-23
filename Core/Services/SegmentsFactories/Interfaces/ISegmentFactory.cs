using Core.Segments.Interfaces;

namespace Core.Services.SegmentsFactories.Interfaces;

internal interface ISegmentFactory
{
    IPiplineSegment Create(Dictionary<string, string> segmentParams);
}
