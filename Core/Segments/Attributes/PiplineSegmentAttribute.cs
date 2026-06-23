namespace Core.Segments.Attributes;

internal class PiplineSegmentAttribute : Attribute
{
    public string Type { get; }

    public PiplineSegmentAttribute(string type)
    {
        Type = type;
    }
}
