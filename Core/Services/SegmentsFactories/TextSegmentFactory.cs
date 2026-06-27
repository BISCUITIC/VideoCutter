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

        var segmentProperties = textSegmentType.GetProperties()
                                               .Select(property => new { Name = property.Name, PropertyType = property.PropertyType });

        object?[] segmentParamsValues = segmentProperties.Select(property =>
        {
            var paramKey = segmentParams.Keys
                .FirstOrDefault(k => string.Equals(k, property.Name, StringComparison.OrdinalIgnoreCase));
           
            if (paramKey != null && segmentParams.TryGetValue(paramKey, out string? value))
            {           
                Console.WriteLine($"{property.Name} = {Convert.ChangeType(value, property.PropertyType)}");
                return Convert.ChangeType(value, property.PropertyType);
            }
            return null;
        }).ToArray();

        TextSegment textSegment = (TextSegment)(Activator.CreateInstance(textSegmentType, segmentParamsValues)
            ?? throw new NullReferenceException($"Couldn't create instatnce of '{textSegmentType}'"));

        return textSegment;
    }
}
