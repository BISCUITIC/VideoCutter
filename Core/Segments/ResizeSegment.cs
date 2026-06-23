using Core.Segments.Interfaces;

namespace Core.Segments;

internal class ResizeSegment : IPiplineSegment
{
    public ResizeSegment()
    {
        Console.WriteLine(this);
    }

    public void Apply()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return "Resize segment";
    }
}
