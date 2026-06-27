using FFMpegCore;
using System.Text;

namespace Core.Segments.Interfaces;

public interface IPiplineSegment
{
    string Apply();
}
