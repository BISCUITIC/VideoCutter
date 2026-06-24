using FFMpegCore;
using System.Text;

namespace Core.Segments.Interfaces;

public interface IPiplineSegment
{
    void Apply(FFMpegArgumentOptions options, StringBuilder filterArgument);
}
