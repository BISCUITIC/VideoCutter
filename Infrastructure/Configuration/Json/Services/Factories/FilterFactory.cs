using Domain.Filters.Attributes;
using Engine.Filters.Interfaces;
using Infrastructure.Configuration.Json.Services.Factories.Interfaces;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;

namespace Infrastructure.Configuration.Json.Services.Factories;

public class FilterFactory : IFilterFactory
{
    private readonly Type _baseFilterType = typeof(IFilter);
    private readonly Type _attributeType = typeof(FilterTypeAttribute);
    private readonly string _assemblyName;
    private readonly IDictionary<string, Type> _filterTypes;

    public FilterFactory()
    {
        _assemblyName = _baseFilterType.Assembly.FullName;
        _filterTypes = InitFilterTypes();
    }

    private IDictionary<string, Type> InitFilterTypes()
    {
        IEnumerable<Type> types =
            Assembly.Load(_assemblyName)
                    .GetTypes()
                    .Where(type => _baseFilterType.IsAssignableFrom(type))
                    .Where(type => type.GetCustomAttribute(_attributeType) is not null);

        foreach(Type type in types) 
            Console.WriteLine(type.Name);

        return types.ToDictionary(type => GetAttributeName(type), type => type);
    }

    private string GetAttributeName(Type type)
    {
        FilterTypeAttribute attribute = (FilterTypeAttribute) type.GetCustomAttribute(_attributeType) ??
                              throw new NullReferenceException("Couldn't get attribute");

        return attribute.Type;
    }

    private IEnumerable<ParameterInfo> GetPropierties(Type type)
    {
         return type.GetConstructors().Single().GetParameters();
    }
    private IEnumerable<object> ParsePrameters(IEnumerable<ParameterInfo> parameters,
                                               IDictionary<string, string> parametersValues)
    {
        if (parametersValues is null)
            throw new Exception("что тоне так ");
        return parameters.Select(parameter =>
            {
                if (parameter.Name is null)
                    throw new Exception("Какаято херня ");
                string? parameterValue;
                bool sucess = parametersValues.TryGetValue(parameter.Name,
                                                           out parameterValue);
                
                if (!sucess || parameterValue is null)
                    throw new KeyNotFoundException($"No '{nameof(parameter.Name)}' property");

                object? value = Convert.ChangeType(parameterValue,
                                                   parameter.ParameterType) ??
                                throw new NullReferenceException("Could not convert property to correct type");

                return value;
            }
        );

    }

    public IFilter Create(string type, IDictionary<string, string> parameterValues)
    {
        bool sucess = _filterTypes.TryGetValue(type, out var filterType);

        if (!sucess || filterType is null)
            throw new KeyNotFoundException($"No '{type}' filter type");

        IEnumerable<ParameterInfo> parameters = GetPropierties(filterType);

        foreach (ParameterInfo value in parameters)
            Console.WriteLine(value.Name + "  " + value.ParameterType.Name);

        IEnumerable<object> values = ParsePrameters(parameters, parameterValues);

        foreach(object value in values)
            Console.WriteLine(value);

        object[] v = { 100, 100 }; 
        return (IFilter) Activator.CreateInstance(filterType, v) ??
            throw new NullReferenceException($"Coulde not create instance of '{nameof(filterType.Name)}'");
    }
}
