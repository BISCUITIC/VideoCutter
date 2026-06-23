using Core.Segments.Attributes;
using Core.Segments.Interfaces;

namespace Core.Segments;

[PiplineSegment("Cut")]
internal class CutSegment : IPiplineSegment
{
    public CutSegment()
    {
        Console.WriteLine(this);
    }

    public void Apply()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return "Cut segment";
    }
}
