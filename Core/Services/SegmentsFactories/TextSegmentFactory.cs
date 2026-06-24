using Core.Segments;
using Core.Segments.Interfaces;
using Core.Services.SegmentsFactories.Attributes;
using Core.Services.SegmentsFactories.Interfaces;

namespace Core.Services.SegmentsFactories;

[SegmentFactory("Text")]
internal class TextSegmentFactory : ISegmentFactory
{
    public TextSegmentFactory() { }

    public IPiplineSegment Create(Dictionary<string, string> segmentParams)
    {
        Type textSegmentType = typeof(TextSegment);

        IEnumerable<string> segmentProperties = textSegmentType.GetProperties().Select(property => property.Name);

        string?[] segmentParamsValues = segmentProperties.Select(propertyName => segmentParams.GetValueOrDefault(propertyName.ToLower())).ToArray();

        foreach (var segmentParam in segmentParamsValues)
            Console.WriteLine(segmentParam);

        TextSegment textSegment = (TextSegment)(Activator.CreateInstance(textSegmentType, segmentParamsValues)
            ?? throw new NullReferenceException($"Couldn't create instatnce of '{textSegmentType}'"));

        return textSegment;
    }
}
