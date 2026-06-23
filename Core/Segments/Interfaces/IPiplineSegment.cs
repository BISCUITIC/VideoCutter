using FFMpegCore;

namespace Core.Segments.Interfaces;

public interface IPiplineSegment
{
    void Apply(FFMpegArgumentOptions options);
}
