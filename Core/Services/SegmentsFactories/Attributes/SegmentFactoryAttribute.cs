namespace Core.Services.SegmentsFactories.Attributes;

internal class SegmentFactoryAttribute : Attribute
{
    public string Type { get; }

    public SegmentFactoryAttribute(string type)
    {
        Type = type;
    }
}
