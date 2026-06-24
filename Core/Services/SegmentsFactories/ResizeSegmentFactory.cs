using Core.Segments;
using Core.Segments.Interfaces;
using Core.Services.SegmentsFactories.Attributes;
using Core.Services.SegmentsFactories.Interfaces;

namespace Core.Services.SegmentsFactories;

[SegmentFactory("Resize")]
internal class ResizeSegmentFactory : ISegmentFactory
{
    public ResizeSegmentFactory() { }

    public IPiplineSegment Create(Dictionary<string, string> segmentParams)
    {
        int width = int.Parse(segmentParams["width"]);

        int height = int.Parse(segmentParams["height"]);

        AspectMode mode = Enum.Parse<AspectMode>(segmentParams["mode"]);

        return new ResizeSegment(width, height, mode);
    }
}
