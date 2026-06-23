using Core.Segments.Attributes;
using Core.Services.Models;
using System.Reflection;

namespace Core.Services;

public class PipelineFactory
{
    private readonly Dictionary<string, Type> _piplineSegmentTypes;

    public PipelineFactory()
    {
        _piplineSegmentTypes = new Dictionary<string, Type>();

        Type[] types = Assembly.GetExecutingAssembly().GetTypes();

        foreach (Type type in types)
        {
            PiplineSegmentAttribute attribute = type.GetCustomAttribute<PiplineSegmentAttribute>();
            if (attribute is not null)
                _piplineSegmentTypes.Add(attribute.Type, type);
        }
    }

    public Pipeline Create(IEnumerable<PipelineSegmentDefinition> segments)
    {
        foreach (PipelineSegmentDefinition definition in segments)
        {
            Type currentType;
            bool success = _piplineSegmentTypes.TryGetValue(definition.Type,out currentType);

            if (success)
            {
                var segment = Activator.CreateInstance(currentType);
            }
        }

        return new Pipeline();
    }
}
