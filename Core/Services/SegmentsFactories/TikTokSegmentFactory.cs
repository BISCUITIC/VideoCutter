using Core.Segments;
using Core.Segments.Interfaces;
using Core.Services.SegmentsFactories.Attributes;
using Core.Services.SegmentsFactories.Interfaces;

namespace Core.Services.SegmentsFactories;

[SegmentFactory("TikTok")]
internal class TikTokSegmentFactory : ISegmentFactory
{
    public TikTokSegmentFactory() { }

    public IPiplineSegment Create(Dictionary<string, string> segmentParams)
    {
        int width = int.Parse(segmentParams["width"]);

        int height = int.Parse(segmentParams["height"]);

        return new TikTokSegment(width, height);
    }
}
