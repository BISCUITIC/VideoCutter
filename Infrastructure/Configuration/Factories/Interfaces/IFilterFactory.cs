using Engine.Filters.Interfaces;

namespace Infrastructure.Configuration.Factories.Interfaces;

public interface IFilterFactory
{
    IFilter Create(string type, IReadOnlyDictionary<string, string> parameters);
}
