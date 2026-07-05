using Domain.Filters.Attributes;
using Engine.Filters.Interfaces;
using Infrastructure.Configuration.Factories.Interfaces;
using System.Data;
using System.Reflection;

namespace Infrastructure.Configuration.Factories;

public class FilterFactory : IFilterFactory
{
    private readonly IReadOnlyDictionary<string, Type> _filterTypes;

    public FilterFactory()
    {        
        _filterTypes = BuildRegistry();
    }

    private IReadOnlyDictionary<string, Type> BuildRegistry()
    {
       return typeof(FilterTypeAttribute)
              .Assembly
              .GetTypes()
              .Where(type => typeof(IFilter).IsAssignableFrom(type))
              .Where(type => type.GetCustomAttribute<FilterTypeAttribute>() is not null)
              .ToDictionary(type => GetAttributeName(type), type => type);
    }

    private string GetAttributeName(Type type)
    {
        return type.GetCustomAttribute<FilterTypeAttribute>()!.Type;
    }
    
    private Type ResolveFilterType(string filterType)
    {
        if (!_filterTypes.TryGetValue(filterType, out var type))
            throw new InvalidOperationException($"Unknown filter '{filterType}'.");

        return type;
    }

    private ConstructorInfo GetConstructor(Type type)
    {
        return type.GetConstructors().Single();
    }

    private object[] BuildArguments(
       ConstructorInfo constructor,
       IReadOnlyDictionary<string, string> parameters)
    {
        return constructor
               .GetParameters()
               .Select(parameter =>
               {
                   if (!parameters.TryGetValue(parameter.Name!, out var value))
                       throw new InvalidOperationException($"Missing parameter '{parameter.Name}'.");

                   return ConvertParameter(value, parameter.ParameterType);
               })
               .ToArray();
    }

    private object ConvertParameter(string value, Type targetType)
    {
        return Convert.ChangeType(value, targetType) ?? 
            throw new InvalidOperationException($"Couldn't convert value '{value}' to type '{targetType.Name}'.");
    }

    public IFilter Create(string type, IReadOnlyDictionary<string, string> parameters)
    {
        Type filterType = ResolveFilterType(type);
        ConstructorInfo constructor = GetConstructor(filterType);

        object[] arguments = BuildArguments(constructor, parameters);

        return (IFilter)constructor.Invoke(arguments);
    }
}
