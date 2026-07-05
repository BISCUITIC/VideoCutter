using Engine.Filters.Interfaces;

namespace Infrastructure.Configuration.Json.Services.Factories.Interfaces;

internal interface IFilterFactory
{
    IFilter Create(string type, IDictionary<string, string> parameterValues);
}
