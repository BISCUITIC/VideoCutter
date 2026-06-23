using Core.Segments.Interfaces;
using Core.Services.Models;
using Core.Services.SegmentsFactories.Attributes;
using Core.Services.SegmentsFactories.Interfaces;
using System.Reflection;

namespace Core.Services;

public class PipelineFactory
{
    private readonly Dictionary<string, ISegmentFactory> _segmentFactories;

    public PipelineFactory()
    {
        _segmentFactories = new Dictionary<string, ISegmentFactory>();

        InitSegmentFactories();
    }

    private void InitSegmentFactories()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();

        foreach (Type type in types)
        {
            if (type.IsInterface || type.IsAbstract)
                continue;

            if (!typeof(ISegmentFactory).IsAssignableFrom(type))
                continue;

            SegmentFactoryAttribute? attribute = type.GetCustomAttribute<SegmentFactoryAttribute>();

            if (attribute is null)
                continue;

            if (Activator.CreateInstance(type) is ISegmentFactory factory)
                _segmentFactories.Add(attribute.Type, factory);
        }
    }

    public Pipeline Create(IEnumerable<PipelineSegmentDefinition> segmentDefinitions)
    {
        List<IPiplineSegment> segments = new List<IPiplineSegment>();

        foreach (PipelineSegmentDefinition definition in segmentDefinitions)
        {
            ISegmentFactory? currentFactory;
            bool success = _segmentFactories.TryGetValue(definition.Type, out currentFactory);

            if (success && currentFactory is not null)
            {                
                segments.Add(currentFactory.Create(definition.SegmentParams));
            }
            else
            {
                throw new InvalidOperationException($"Unknown segment type '{definition.Type}'");
            }
        }

        return new Pipeline(segments);
    }
}
